Shader "Custom/WindWakerWater" {
	Properties {
		_WaterCol ("Water Color", Vector) = (0.04,0.38,0.88,1)
		_Water2Col ("Water2 Color", Vector) = (0.04,0.35,0.78,1)
		_FoamCol ("Foam Color", Vector) = (0.8125,0.9609,0.9648,1)
		_DistortionSpeed ("Distortion Speed", Range(0, 10)) = 2
		_Tile ("Tile", Vector) = (5,5,0,0)
		_Height ("Wave Height", Range(0, 10)) = 2
		_WaveSize ("Wave Size", Vector) = (2,2,0,0)
		_WaveSpeed ("Wave Speed", Range(0, 10)) = 1
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
}