Shader "GOCD/SkyboxBlend" {
	Properties {
		_Skybox1 ("Skybox 1", Cube) = "" {}
		_Skybox2 ("Skybox 2", Cube) = "" {}
		_Blend ("Blend", Range(0, 1)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "RenderFX/Skybox"
}