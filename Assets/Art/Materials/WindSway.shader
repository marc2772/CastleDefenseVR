
    Shader "Custom/WindSway" {
        Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _MainColor ("Color", Color) = (1,1,1,1)
        _MinY("Minimum Y Value", float) = 0.0 

        _xScale ("X Amount", Range(-1,1)) = 0.5
        _yScale ("Z Amount", Range(-1,1)) = 0.5

        _Scale("Effect Scale", float) = 1.0 
        _Speed("Effect Speed", float) = 1.0 

        _WorldScale("World Scale", float) = 1.0 
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow
        #pragma target 3.0
        #include "UnityCG.cginc"

        	sampler2D _MainTex;
            float4 _MainColor;
            float _MinY;
            float _xScale;
            float _yScale;
            float _Scale;
            float _WorldScale;
            float _Speed;

        struct Input {
            float2 uv_MainTex;
        };

        float _Amount;

        void vert (inout appdata_full v) {
                float num = v.vertex.z;

                if((num-_MinY) > 0.0){
                    float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                    float x = sin(worldPos.x/_WorldScale + (_Time.y*_Speed)) * (num-_MinY) * _Scale * 0.01;
                    float y = sin(worldPos.y/_WorldScale + (_Time.y*_Speed)) * (num-_MinY) * _Scale * 0.01;

                    v.vertex.x += x * _xScale;
                    v.vertex.y += y * _yScale;
                }
        }

		void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _MainColor * tex2D(_MainTex, IN.uv_MainTex).rgb;
        }

        ENDCG
    } 
    Fallback "VertexLit"
}


