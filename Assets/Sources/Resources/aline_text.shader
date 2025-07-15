Shader "Hidden/ALINE/Font" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,0.5)
		_FadeColor ("Fade Color", Vector) = (1,1,1,0.3)
		_MainTex ("Texture", 2D) = "white" {}
		_FallbackTex ("Fallback Texture", 2D) = "white" {}
		_FallbackAmount ("Fallback Amount", Range(0, 1)) = 1
		_TransitionPoint ("Transition Point", Range(0, 5)) = 0.6
		_MipBias ("Mip Bias", Range(-2, 0)) = -1
		_GammaCorrection ("Gamma Correction", Range(0, 2)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}