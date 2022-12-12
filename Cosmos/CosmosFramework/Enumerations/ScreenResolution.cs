namespace CosmosFramework
{
	/// <summary>
	/// Contains defintions of some default 16:9 screen resolutions.
	/// </summary>
	public enum ScreenResolution
	{
		/// <summary>640 x 360</summary>
		m_360p,
		/// <summary>854 x 480</summary>
		m_480p,
		/// <summary>960 x 540</summary>
		m_540p,
		/// <summary>1280 x 720</summary>
		m_720p,
		/// <summary>1600 x 900</summary>
		m_900p,
		/// <summary>1920 x 1080</summary>
		m_1080p,
		/// <summary>2560 x 1440</summary>
		m_1440p,
		/// <summary>3840 x 2160</summary>
		m_2160p,
	}

	public static class ScreenResolutionExtension
	{
		public static int Width(this ScreenResolution resolution) => resolution switch
		{
			ScreenResolution.m_360p => 640,
			ScreenResolution.m_480p => 854,
			ScreenResolution.m_540p => 960,
			ScreenResolution.m_720p => 1280,
			ScreenResolution.m_900p => 1600,
			ScreenResolution.m_1080p => 1920,
			ScreenResolution.m_1440p => 2560,
			ScreenResolution.m_2160p => 3840,
		};
		public static int Height(this ScreenResolution resolution) => resolution switch
		{
			ScreenResolution.m_360p => 360,
			ScreenResolution.m_480p => 480,
			ScreenResolution.m_540p => 540,
			ScreenResolution.m_720p => 720,
			ScreenResolution.m_900p => 900,
			ScreenResolution.m_1080p => 1080,
			ScreenResolution.m_1440p => 1440,
			ScreenResolution.m_2160p => 2160,
		};
	}
}