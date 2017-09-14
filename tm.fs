precision mediump float;

varying vec2        v_texCoord;

uniform sampler2D   u_image0;   // original
uniform sampler2D   u_image1;   // template

uniform vec2        u_ires0;
uniform vec2        u_ires1;

void main()
{
    float sumR = 0.0;
    float sumG = 0.0;
    float sumB = 0.0;
    
    // the 'sliding window' (= template image) is 32x32.
    for (int i = 0; i <= 32; i++)
    {
        for (int j = 0; j <= 32; j++)
        {
            // calculate the amount in pixels to step in x and y for the original and template images.
            vec2 d0 = vec2(float(i), float(j)) * u_ires0;
            vec2 d1 = vec2(float(i), float(j)) * u_ires1;

            // retrieve the corresponding texels from the images.
            vec4 I = texture2D(u_image0, v_texCoord + d0);
            vec4 T = texture2D(u_image1, d1);

            // calculate the SQDIFF metric.

            float diffR = T.r - I.r;
            float diffG = T.g - I.g;
            float diffB = T.b - I.b;

            sumR += diffR * diffR;
            sumG += diffG * diffG;
            sumB += diffB * diffB;
        }
    }

    // set the (normalized) sum in the result matrix.
    gl_FragColor = vec4(sumR / float(255), sumG / float(255), sumB / float(255), 1);
}