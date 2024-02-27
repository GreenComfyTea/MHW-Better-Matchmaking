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
	protected readonly LocalizationManager localizationManager = LocalizationManager.Instance;
	[JsonIgnore]
	protected readonly ConfigManager configManager = ConfigManager.Instance;
	[JsonIgnore]
	protected readonly RegionLockFix regionLockFix = RegionLockFix.Instance;
	[JsonIgnore]
	protected readonly MaxSearchResultLimit maxSearchResultLimit = MaxSearchResultLimit.Instance;
}
