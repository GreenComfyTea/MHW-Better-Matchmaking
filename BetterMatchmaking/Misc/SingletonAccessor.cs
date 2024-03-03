using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetterMatchmaking;

internal class SingletonAccessor
{
	[JsonIgnore]
	protected LocalizationManager LocalizationManagerInstance { get; set; }
	[JsonIgnore]
	protected ConfigManager ConfigManagerInstance { get; set; }
	[JsonIgnore]
	protected RegionLockFix RegionLockFixInstance { get; set; }
	[JsonIgnore]
	protected MaxSearchResultLimit MaxSearchResultLimitInstance { get; set; }
	[JsonIgnore]
	protected SessionPlayerCountFilter SessionPlayerCountFilterInstance { get; set; }
	[JsonIgnore]
	protected Core CoreInstance { get; set; }
	[JsonIgnore]
	protected CustomizationWindow CustomizationWindowInstance { get; set; }
	[JsonIgnore]
	protected DebugManager DebugManagerInstance { get; set; }

	protected void InstantiateSingletons()
	{
		LocalizationManagerInstance = LocalizationManager.Instance;
		ConfigManagerInstance = ConfigManager.Instance;
		RegionLockFixInstance = RegionLockFix.Instance;
		MaxSearchResultLimitInstance = MaxSearchResultLimit.Instance;
		SessionPlayerCountFilterInstance = SessionPlayerCountFilter.Instance;
		CoreInstance = Core.Instance;
		CustomizationWindowInstance = CustomizationWindow.Instance;
		DebugManagerInstance = DebugManager.Instance;
	}
}
