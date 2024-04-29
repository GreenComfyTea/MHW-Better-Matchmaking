using SharpPluginLoader.Core.IO;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.Steam;

namespace BetterMatchmaking;

internal sealed class Core : SingletonAccessor, IDisposable
{
	// Singleton Pattern
	private static readonly Core _singleton = new();

	public static Core Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static Core() { }

	// Singleton Pattern End

	public SearchTypes CurrentSearchType { get; set; } = SearchTypes.None;

	public bool IsLanguageAny { get; set; } = false;
	public bool IsQuestTypeNoPreference { get; set; } = false;
	public bool IsQuestRewardsNoPreference { get; set; } = false;
	public bool IsExpeditionObjectiveNoPreference { get; set; } = false;
	public bool IsRegionLevelNoPreference { get; set; } = false;
	public bool IsTargetMonsterNoPreference { get; set; } = false;

	private delegate int startRequest_Delegate(nint netCore, nint netRequest);
	private Hook<startRequest_Delegate> StartRequestHook { get; set; }

	private delegate void numericalFilter_Delegate(nint steamInterface, nint keyAddress, int value, int comparison);
	private Hook<numericalFilter_Delegate> NumericalFilterHook { get; set; }

	private nint SteamMatchmakingInterface { get; set; }



	private delegate int AddFavoriteGame_Delegate(nint steamInterface, uint appID, UInt16 connectionPort, UInt16 queryPort, uint flags, uint timeLastPlayedOnServer);
	private delegate void AddRequestLobbyListCompatibleMembersFilter_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate void AddRequestLobbyListDistanceFilter_Delegate(nint steamInterface, int lobbyDistanceFilter);
	private delegate void AddRequestLobbyListFilterSlotsAvailable_Delegate(nint steamInterface, int slotsAvailable);
	private delegate void AddRequestLobbyListNearValueFilter_Delegate(nint steamInterface, nint keyToMatch, int valueToBeCloseTo);
	private delegate void AddRequestLobbyListResultCountFilter_Delegate(nint steamInterface, int maxResults);
	private delegate void AddRequestLobbyListStringFilter_Delegate(nint steamInterface, nint keyToMatch, nint valueToMatch, int comparisonType);
	private delegate nint CreateLobby_Delegate(nint steamInterface, int lobbyType, int maxMembers);
	private delegate bool DeleteLobbyData_Delegate(nint steamInterface, uint steamIDLobby, nint key);
	private delegate bool GetFavoriteGame_Delegate(nint steamInterface, int game, nint appID, nint ip, nint connectionPort, nint queryPort, nint flags, nint timeLastPlayedOnServer);
	private delegate int GetFavoriteGameCount_Delegate(nint steamInterface);
	private delegate uint GetLobbyByIndex_Delegate(nint steamInterface, int lobby);
	private delegate int GetLobbyChatEntry_Delegate(nint steamInterface, uint steamIDLobby, int chatID, nint steamIDUser, nint data, int cubData, nint chatEntryType);
	private delegate nint GetLobbyData_Delegate(nint steamInterface, uint steamIDLobby, nint key);
	private delegate bool GetLobbyDataByIndex_Delegate(nint steamInterface, uint steamIDLobby, int lobbyData, nint key, int keyBufferSize, nint value, int valueBufferSize);
	private delegate int GetLobbyDataCount_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate bool GetLobbyGameServer_Delegate(nint steamInterface, uint steamIDLobby, nint gameServerIP, nint gameServerPort, nint steamIDGameServer);
	private delegate uint GetLobbyMemberByIndex_Delegate(nint steamInterface, uint steamIDLobby, int member);
	private delegate nint GetLobbyMemberData_Delegate(nint steamInterface, uint steamIDLobby, uint steamIDUser, nint key);
	private delegate int GetLobbyMemberLimit_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate uint GetLobbyOwner_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate int GetNumLobbyMembers_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate bool InviteUserToLobby_Delegate(nint steamInterface, uint steamIDLobby, uint steamIDInvitee);
	private delegate nint JoinLobby_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate void LeaveLobby_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate bool RemoveFavoriteGame_Delegate(nint steamInterface, uint appID, uint ip, UInt16 connectionPort, UInt16 queryPort, uint flags);
	private delegate bool RequestLobbyData_Delegate(nint steamInterface, uint steamIDLobby);
	private delegate nint RequestLobbyList_Delegate(nint steamInterface);
	private delegate bool SendLobbyChatMessage_Delegate(nint steamInterface, uint steamIDLobby, nint messageBody, int cubMessageBody);
	private delegate bool SetLinkedLobby_Delegate(nint steamInterface, uint steamIDLobby, uint steamIDLobbyDependent);
	private delegate bool SetLobbyData_Delegate(nint steamInterface, uint steamIDLobby, nint key, nint value);
	private delegate void SetLobbyGameServer_Delegate(nint steamInterface, uint steamIDLobby, uint gameServerIP, UInt16 gameServerPort, uint steamIDGameServer);
	private delegate bool SetLobbyJoinable_Delegate(nint steamInterface, uint steamIDLobby, bool lobbyJoinable);
	private delegate void SetLobbyMemberData_Delegate(nint steamInterface, uint steamIDLobby, nint key, nint value);
	private delegate bool SetLobbyMemberLimit_Delegate(nint steamInterface, uint steamIDLobby, int maxMembers);
	private delegate bool SetLobbyOwner_Delegate(nint steamInterface, uint steamIDLobby, uint steamIDnewOwner);
	private delegate bool SetLobbyType_Delegate(nint steamInterface, uint steamIDLobby, int lobbyType);



	private Hook<AddFavoriteGame_Delegate> AddFavoriteGame_Hook { get; set; }
	private Hook<AddRequestLobbyListCompatibleMembersFilter_Delegate> AddRequestLobbyListCompatibleMembersFilter_Hook { get; set; }
	private Hook<AddRequestLobbyListDistanceFilter_Delegate> AddRequestLobbyListDistanceFilter_Hook { get; set; }
	private Hook<AddRequestLobbyListFilterSlotsAvailable_Delegate> AddRequestLobbyListFilterSlotsAvailable_Hook { get; set; }
	private Hook<AddRequestLobbyListNearValueFilter_Delegate> AddRequestLobbyListNearValueFilter_Hook { get; set; }
	private Hook<AddRequestLobbyListResultCountFilter_Delegate> AddRequestLobbyListResultCountFilter_Hook { get; set; }
	private Hook<AddRequestLobbyListStringFilter_Delegate> AddRequestLobbyListStringFilter_Hook { get; set; }
	private Hook<CreateLobby_Delegate> CreateLobby_Hook { get; set; }
	private Hook<DeleteLobbyData_Delegate> DeleteLobbyData_Hook { get; set; }
	private Hook<GetFavoriteGame_Delegate> GetFavoriteGame_Hook { get; set; }
	private Hook<GetFavoriteGameCount_Delegate> GetFavoriteGameCount_Hook { get; set; }
	private Hook<GetLobbyByIndex_Delegate> GetLobbyByIndex_Hook { get; set; }
	private Hook<GetLobbyChatEntry_Delegate> GetLobbyChatEntry_Hook { get; set; }
	private Hook<GetLobbyData_Delegate> GetLobbyData_Hook { get; set; }
	private Hook<GetLobbyDataByIndex_Delegate> GetLobbyDataByIndex_Hook { get; set; }
	private Hook<GetLobbyDataCount_Delegate> GetLobbyDataCount_Hook { get; set; }
	private Hook<GetLobbyGameServer_Delegate> GetLobbyGameServer_Hook { get; set; }
	private Hook<GetLobbyMemberByIndex_Delegate> GetLobbyMemberByIndex_Hook { get; set; }
	private Hook<GetLobbyMemberData_Delegate> GetLobbyMemberData_Hook { get; set; }
	private Hook<GetLobbyMemberLimit_Delegate> GetLobbyMemberLimit_Hook { get; set; }
	private Hook<GetLobbyOwner_Delegate> GetLobbyOwner_Hook { get; set; }
	private Hook<GetNumLobbyMembers_Delegate> GetNumLobbyMembers_Hook { get; set; }
	private Hook<InviteUserToLobby_Delegate> InviteUserToLobby_Hook { get; set; }
	private Hook<JoinLobby_Delegate> JoinLobby_Hook { get; set; }
	private Hook<LeaveLobby_Delegate> LeaveLobby_Hook { get; set; }
	private Hook<RemoveFavoriteGame_Delegate> RemoveFavoriteGame_Hook { get; set; }
	private Hook<RequestLobbyData_Delegate> RequestLobbyData_Hook { get; set; }
	private Hook<RequestLobbyList_Delegate> RequestLobbyList_Hook { get; set; }
	private Hook<SendLobbyChatMessage_Delegate> SendLobbyChatMessage_Hook { get; set; }
	private Hook<SetLinkedLobby_Delegate> SetLinkedLobby_Hook { get; set; }
	private Hook<SetLobbyData_Delegate> SetLobbyData_Hook { get; set; }
	private Hook<SetLobbyGameServer_Delegate> SetLobbyGameServer_Hook { get; set; }
	private Hook<SetLobbyJoinable_Delegate> SetLobbyJoinable_Hook { get; set; }
	private Hook<SetLobbyMemberData_Delegate> SetLobbyMemberData_Hook { get; set; }
	private Hook<SetLobbyMemberLimit_Delegate> SetLobbyMemberLimit_Hook { get; set; }
	private Hook<SetLobbyOwner_Delegate> SetLobbyOwner_Hook { get; set; }
	private Hook<SetLobbyType_Delegate> SetLobbyType_Hook { get; set; }


	private bool IsDebugHooksInitialized { get; set; } = false;

	private Core() { }

	public void InitDebugHooks()
	{
		if(IsDebugHooksInitialized) return;

		TeaLog.Warn("InitDebugHooks");
		IsDebugHooksInitialized = true;

		var AddFavoriteGame_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddFavoriteGame);
		var AddRequestLobbyListCompatibleMembersFilter_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListCompatibleMembersFilter);
		var AddRequestLobbyListDistanceFilter_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListDistanceFilter);
		var AddRequestLobbyListFilterSlotsAvailable_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListFilterSlotsAvailable);
		var AddRequestLobbyListNearValueFilter_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListNearValueFilter);
		var AddRequestLobbyListResultCountFilter_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListResultCountFilter);
		var AddRequestLobbyListStringFilter_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListStringFilter);
		var CreateLobby_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.CreateLobby);
		var DeleteLobbyData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.DeleteLobbyData);
		var GetFavoriteGame_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetFavoriteGame);
		var GetFavoriteGameCount_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetFavoriteGameCount);
		var GetLobbyByIndex_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyByIndex);
		var GetLobbyChatEntry_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyChatEntry);
		var GetLobbyData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyData);
		var GetLobbyDataByIndex_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyDataByIndex);
		var GetLobbyDataCount_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyDataCount);
		var GetLobbyGameServer_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyGameServer);
		var GetLobbyMemberByIndex_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyMemberByIndex);
		var GetLobbyMemberData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyMemberData);
		var GetLobbyMemberLimit_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyMemberLimit);
		var GetLobbyOwner_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetLobbyOwner);
		var GetNumLobbyMembers_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.GetNumLobbyMembers);
		var InviteUserToLobby_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.InviteUserToLobby);
		var JoinLobby_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.JoinLobby);
		var LeaveLobby_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.LeaveLobby);
		var RemoveFavoriteGame_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.RemoveFavoriteGame);
		var RequestLobbyData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.RequestLobbyData);
		var RequestLobbyList_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.RequestLobbyList);
		var SendLobbyChatMessage_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SendLobbyChatMessage);
		var SetLinkedLobby_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLinkedLobby);
		var SetLobbyData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyData);
		var SetLobbyGameServer_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyGameServer);
		var SetLobbyJoinable_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyJoinable);
		var SetLobbyMemberData_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyMemberData);
		var SetLobbyMemberLimit_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyMemberLimit);
		var SetLobbyOwner_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyOwner);
		var SetLobbyType_Address = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.SetLobbyType);

		AddFavoriteGame_Hook = Hook.Create<AddFavoriteGame_Delegate>(AddFavoriteGame_Address,
		(steamInterface, appID, connectionPort, queryPort, flags, timeLastPlayedOnServer) =>
		{
			TeaLog.Warn("AddFavoriteGame");
			return AddFavoriteGame_Hook!.Original(steamInterface, appID, connectionPort, queryPort, flags, timeLastPlayedOnServer);
		});

		AddRequestLobbyListCompatibleMembersFilter_Hook = Hook.Create<AddRequestLobbyListCompatibleMembersFilter_Delegate>(AddRequestLobbyListCompatibleMembersFilter_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("AddRequestLobbyListCompatibleMembersFilter");
			AddRequestLobbyListCompatibleMembersFilter_Hook!.Original(steamInterface, steamIDLobby);
		});

		AddRequestLobbyListDistanceFilter_Hook = Hook.Create<AddRequestLobbyListDistanceFilter_Delegate>(AddRequestLobbyListDistanceFilter_Address,
		(steamInterface, lobbyDistanceFilter) =>
		{
			TeaLog.Warn("AddRequestLobbyListDistanceFilter");
			AddRequestLobbyListDistanceFilter_Hook!.Original(steamInterface, lobbyDistanceFilter);
		});

		AddRequestLobbyListFilterSlotsAvailable_Hook = Hook.Create<AddRequestLobbyListFilterSlotsAvailable_Delegate>(AddRequestLobbyListFilterSlotsAvailable_Address,
		(steamInterface, slotsAvailable) =>
		{
			TeaLog.Warn("AddRequestLobbyListFilterSlotsAvailable");
			AddRequestLobbyListFilterSlotsAvailable_Hook!.Original(steamInterface, slotsAvailable);
		});

		AddRequestLobbyListNearValueFilter_Hook = Hook.Create<AddRequestLobbyListNearValueFilter_Delegate>(AddRequestLobbyListNearValueFilter_Address,
		(steamInterface, keyToMatch, valueToBeCloseTo) =>
		{
			TeaLog.Warn("AddRequestLobbyListNearValueFilter");
			AddRequestLobbyListNearValueFilter_Hook!.Original(steamInterface, keyToMatch, valueToBeCloseTo);
		});

		AddRequestLobbyListResultCountFilter_Hook = Hook.Create<AddRequestLobbyListResultCountFilter_Delegate>(AddRequestLobbyListResultCountFilter_Address,
		(steamInterface, maxResults) =>
		{
			TeaLog.Warn("AddRequestLobbyListResultCountFilter");
			AddRequestLobbyListResultCountFilter_Hook!.Original(steamInterface, maxResults);
		});

		AddRequestLobbyListStringFilter_Hook = Hook.Create<AddRequestLobbyListStringFilter_Delegate>(AddRequestLobbyListStringFilter_Address,
		(steamInterface, keyToMatch, valueToMatch, comparisonType) =>
		{
			TeaLog.Warn("AddRequestLobbyListStringFilter");
			AddRequestLobbyListStringFilter_Hook!.Original(steamInterface, keyToMatch, valueToMatch, comparisonType);
		});

		CreateLobby_Hook = Hook.Create<CreateLobby_Delegate>(CreateLobby_Address,
		(steamInterface, lobbyType, maxMembers) =>
		{
			TeaLog.Warn("CreateLobby");
			return CreateLobby_Hook!.Original(steamInterface, lobbyType, maxMembers);
		});

		DeleteLobbyData_Hook = Hook.Create<DeleteLobbyData_Delegate>(DeleteLobbyData_Address,
		(steamInterface, steamIDLobby, key) =>
		{
			TeaLog.Warn("DeleteLobbyData");
			return DeleteLobbyData_Hook!.Original(steamInterface, steamIDLobby, key);
		});

		GetFavoriteGame_Hook = Hook.Create<GetFavoriteGame_Delegate>(GetFavoriteGame_Address,
		(steamInterface, game, appID, ip, connectionPort, queryPort, flags, timeLastPlayedOnServer) =>
		{
			TeaLog.Warn("GetFavoriteGame");
			return GetFavoriteGame_Hook!.Original(steamInterface, game, appID, ip, connectionPort, queryPort, flags, timeLastPlayedOnServer);
		});

		GetFavoriteGameCount_Hook = Hook.Create<GetFavoriteGameCount_Delegate>(GetFavoriteGameCount_Address,
		(steamInterface) =>
		{
			TeaLog.Warn("GetFavoriteGameCount");
			return GetFavoriteGameCount_Hook!.Original(steamInterface);
		});

		GetLobbyByIndex_Hook = Hook.Create<GetLobbyByIndex_Delegate>(GetLobbyByIndex_Address,
		(steamInterface, lobby) =>
		{
			TeaLog.Warn("GetLobbyByIndex");
			return GetLobbyByIndex_Hook!.Original(steamInterface, lobby);
		});

		GetLobbyChatEntry_Hook = Hook.Create<GetLobbyChatEntry_Delegate>(GetLobbyChatEntry_Address,
		(steamInterface, steamIDLobby, chatID, steamIDUser, data, cubData, chatEntryType) =>
		{
			TeaLog.Warn("GetLobbyChatEntry");
			return GetLobbyChatEntry_Hook!.Original(steamInterface, steamIDLobby, chatID, steamIDUser, data, cubData, chatEntryType);
		});

		GetLobbyData_Hook = Hook.Create<GetLobbyData_Delegate>(GetLobbyData_Address,
		(steamInterface, steamIDLobby, key) =>
		{
			TeaLog.Warn("GetLobbyData");
			return GetLobbyData_Hook!.Original(steamInterface, steamIDLobby, key);
		});

		GetLobbyDataByIndex_Hook = Hook.Create<GetLobbyDataByIndex_Delegate>(GetLobbyDataByIndex_Address,
		(steamInterface, steamIDLobby, lobbyData, key, keyBufferSize, value, valueBufferSize) =>
		{
			TeaLog.Warn("GetLobbyDataByIndex");
			return GetLobbyDataByIndex_Hook!.Original(steamInterface, steamIDLobby, lobbyData, key, keyBufferSize, value, valueBufferSize);
		});

		GetLobbyDataCount_Hook = Hook.Create<GetLobbyDataCount_Delegate>(GetLobbyDataCount_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("GetLobbyDataCount");
			return GetLobbyDataCount_Hook!.Original(steamInterface, steamIDLobby);
		});

		GetLobbyGameServer_Hook = Hook.Create<GetLobbyGameServer_Delegate>(GetLobbyGameServer_Address,
		(steamInterface, steamIDLobby, gameServerIP, gameServerPort, steamIDGameServer) =>
		{
			TeaLog.Warn("GetLobbyGameServer");
			return GetLobbyGameServer_Hook!.Original(steamInterface, steamIDLobby, gameServerIP, gameServerPort, steamIDGameServer);
		});

		GetLobbyMemberByIndex_Hook = Hook.Create<GetLobbyMemberByIndex_Delegate>(GetLobbyMemberByIndex_Address,
		(steamInterface, steamIDLobby, member) =>
		{
			TeaLog.Warn("GetLobbyMemberByIndex");
			return GetLobbyMemberByIndex_Hook!.Original(steamInterface, steamIDLobby, member);
		});

		GetLobbyMemberData_Hook = Hook.Create<GetLobbyMemberData_Delegate>(GetLobbyMemberData_Address,
		(steamInterface, steamIDLobby, steamIDUser, key) =>
		{
			TeaLog.Warn("GetLobbyMemberData");
			return GetLobbyMemberData_Hook!.Original(steamInterface, steamIDLobby, steamIDUser, key);
		});

		GetLobbyMemberLimit_Hook = Hook.Create<GetLobbyMemberLimit_Delegate>(GetLobbyMemberLimit_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("GetLobbyMemberLimit");
			return GetLobbyMemberLimit_Hook!.Original(steamInterface, steamIDLobby);
		});

		GetLobbyOwner_Hook = Hook.Create<GetLobbyOwner_Delegate>(GetLobbyOwner_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("GetLobbyOwner");
			return GetLobbyOwner_Hook!.Original(steamInterface, steamIDLobby);
		});

		GetNumLobbyMembers_Hook = Hook.Create<GetNumLobbyMembers_Delegate>(GetNumLobbyMembers_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("GetNumLobbyMembers");
			return GetNumLobbyMembers_Hook!.Original(steamInterface, steamIDLobby);
		});

		InviteUserToLobby_Hook = Hook.Create<InviteUserToLobby_Delegate>(InviteUserToLobby_Address,
		(steamInterface, steamIDLobby, steamIDInvitee) =>
		{
			TeaLog.Warn("InviteUserToLobby");
			return InviteUserToLobby_Hook!.Original(steamInterface, steamIDLobby, steamIDInvitee);
		});

		JoinLobby_Hook = Hook.Create<JoinLobby_Delegate>(JoinLobby_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("JoinLobby");
			return JoinLobby_Hook!.Original(steamInterface, steamIDLobby);
		});

		LeaveLobby_Hook = Hook.Create<LeaveLobby_Delegate>(LeaveLobby_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("LeaveLobby");
			LeaveLobby_Hook!.Original(steamInterface, steamIDLobby);
		});

		RemoveFavoriteGame_Hook = Hook.Create<RemoveFavoriteGame_Delegate>(RemoveFavoriteGame_Address,
		(steamInterface, appID, ip, connectionPort, queryPort, flags) =>
		{
			TeaLog.Warn("RemoveFavoriteGame");
			return RemoveFavoriteGame_Hook!.Original(steamInterface, appID, ip, connectionPort, queryPort, flags);
		});

		RequestLobbyData_Hook = Hook.Create<RequestLobbyData_Delegate>(RequestLobbyData_Address,
		(steamInterface, steamIDLobby) =>
		{
			TeaLog.Warn("RequestLobbyData");
			return RequestLobbyData_Hook!.Original(steamInterface, steamIDLobby);
		});

		RequestLobbyList_Hook = Hook.Create<RequestLobbyList_Delegate>(RequestLobbyList_Address,
		(steamInterface) =>
		{
			TeaLog.Warn("RequestLobbyList");
			return RequestLobbyList_Hook!.Original(steamInterface);
		});

		SendLobbyChatMessage_Hook = Hook.Create<SendLobbyChatMessage_Delegate>(SendLobbyChatMessage_Address,
		(steamInterface, steamIDLobby, messageBody, cubMessageBody) =>
		{
			TeaLog.Warn("SendLobbyChatMessage");
			return SendLobbyChatMessage_Hook!.Original(steamInterface, steamIDLobby, messageBody, cubMessageBody);
		});

		SetLinkedLobby_Hook = Hook.Create<SetLinkedLobby_Delegate>(SetLinkedLobby_Address,
		(steamInterface, steamIDLobby, steamIDLobbyDependent) =>
		{
			TeaLog.Warn("SetLinkedLobby");
			return SetLinkedLobby_Hook!.Original(steamInterface, steamIDLobby, steamIDLobbyDependent);
		});

		SetLobbyData_Hook = Hook.Create<SetLobbyData_Delegate>(SetLobbyData_Address,
		(steamInterface, steamIDLobby, key, value) =>
		{
			TeaLog.Warn("SetLobbyData");
			return SetLobbyData_Hook!.Original(steamInterface, steamIDLobby, key, value);
		});

		SetLobbyGameServer_Hook = Hook.Create<SetLobbyGameServer_Delegate>(SetLobbyGameServer_Address,
		(steamInterface, steamIDLobby, gameServerIP, gameServerPort, steamIDGameServer) =>
		{
			TeaLog.Warn("SetLobbyGameServer");
			SetLobbyGameServer_Hook!.Original(steamInterface, steamIDLobby, gameServerIP, gameServerPort, steamIDGameServer);
		});

		SetLobbyJoinable_Hook = Hook.Create<SetLobbyJoinable_Delegate>(SetLobbyJoinable_Address,
		(steamInterface, steamIDLobby, lobbyJoinable) =>
		{
			TeaLog.Warn("SetLobbyJoinable");
			return SetLobbyJoinable_Hook!.Original(steamInterface, steamIDLobby, lobbyJoinable);
		});

		SetLobbyMemberData_Hook = Hook.Create<SetLobbyMemberData_Delegate>(SetLobbyMemberData_Address,
		(steamInterface, steamIDLobby, key, value) =>
		{
			TeaLog.Warn("SetLobbyMemberData");
			SetLobbyMemberData_Hook!.Original(steamInterface, steamIDLobby, key, value);
		});

		SetLobbyMemberLimit_Hook = Hook.Create<SetLobbyMemberLimit_Delegate>(SetLobbyMemberLimit_Address,
		(steamInterface, steamIDLobby, maxMembers) =>
		{
			TeaLog.Warn("SetLobbyMemberLimit");
			return SetLobbyMemberLimit_Hook!.Original(steamInterface, steamIDLobby, maxMembers);
		});

		SetLobbyOwner_Hook = Hook.Create<SetLobbyOwner_Delegate>(SetLobbyOwner_Address,
		(steamInterface, steamIDLobby, steamIDnewOwner) =>
		{
			TeaLog.Warn("SetLobbyOwner");
			return SetLobbyOwner_Hook!.Original(steamInterface, steamIDLobby, steamIDnewOwner);
		});

		SetLobbyType_Hook = Hook.Create<SetLobbyType_Delegate>(SetLobbyType_Address,
		(steamInterface, steamIDLobby, lobbyType) =>
		{
			TeaLog.Warn("SetLobbyType");
			return SetLobbyType_Hook!.Original(steamInterface, steamIDLobby, lobbyType);
		});
	}

	public Core Init()
	{
		TeaLog.Info("Core: Initializing...");

		InstantiateSingletons();

		TeaLog.Info("Core: Scanning for StartRequest() Address...");

		var instruction = PatternScanner.FindFirst(Pattern.FromString(Constants.START_REQUEST_FUNCTION_PATTERN));
		var startRequestAddress = instruction + Constants.START_REQUEST_FUNCTION_OFFSET;

		if(startRequestAddress == 0)
		{
			TeaLog.Info("Core: StartRequest() Not Found!");
		}
		else
		{
			TeaLog.Info($"Core: StartRequest() Found at 0x{startRequestAddress:X}!");
			TeaLog.Info("Core: Creating StartRequest() Hook...");
			StartRequestHook = Hook.Create<startRequest_Delegate>(startRequestAddress, OnStartRequest);
			TeaLog.Info("Core: StartRequest() Hook Created!");
		}

		TeaLog.Info("Core: Creating AddRequestLobbyListNumericalFilter() Hook...");

		var numericalFilterAddress = Matchmaking.GetVirtualFunction(Matchmaking.VirtualFunctionIndex.AddRequestLobbyListNumericalFilter);

		//Causes Friend Session Search to not work
		NumericalFilterHook = Hook.Create<numericalFilter_Delegate>(numericalFilterAddress, (steamInterface, keyAddress, value, comparison) =>
		{
			TeaLog.Warn("NumericalFilter");
			NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
		});

		TeaLog.Info("Core: AddRequestLobbyListNumericalFilter() Hook Created!");

		SteamMatchmakingInterface = Matchmaking.GetSteamMatchmakingInterface();

		TeaLog.Info("Core: Initialization Done!");

		return this;
	}

	public string GetSearchKeyName(string key)
	{
		if(CurrentSearchType == SearchTypes.Quest)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "???";
			if(key == "SearchKey2") return "Rewards Available";
			if(key == "SearchKey3") return "Target";
			if(key == "SearchKey4") return "Difficulty";
			if(key == "SearchKey5") return "Language";
			if(key == "SearchKey6") return "Quest Type";
			if(key == "SearchKey7") return "???";
			if(key == "SearchKey8") return "???";

			return "";
		}

		if(CurrentSearchType == SearchTypes.Session)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "Player Type";
			if(key == "SearchKey2") return "Quest Preference";
			if(key == "SearchKey3") return "???";
			if(key == "SearchKey4") return "Language";
			if(key == "SearchKey5") return "Similar Hunter Rank";
			if(key == "SearchKey6") return "Similar Master Rank";
			if(key == "SearchKey7") return "Min Master Rank";
			if(key == "SearchKey8") return "Max Master Rank";

			return "";
		}

		if(CurrentSearchType == SearchTypes.GuidingLands)
		{
			if(key == "SearchKey0") return "Search Type";
			if(key == "SearchKey1") return "Guiding Lands";
			if(key == "SearchKey2") return "Expedition Objective";
			if(key == "SearchKey3") return "Target Monster";
			if(key == "SearchKey4") return "Conditions";
			if(key == "SearchKey5") return "???";
			if(key == "SearchKey6") return "Region Level";
			if(key == "SearchKey7") return "???";
			if(key == "SearchKey8") return "???";

			return "";
		}

		return "";
	}

	public static string GetComparisonSign(int comparison)
	{
		if (comparison == -2) return "<=";
		if (comparison == -1) return "<";
		if (comparison == 0) return "==";
		if (comparison == 1) return ">";
		if (comparison == 2) return ">=";
		if (comparison == 3) return "!=";

		return "";
	}

	private void AnalyzeSearchKeys(nint netRequest)
	{
		var requestArgumentsAddress = MemoryUtil.Read<nint>(netRequest + 0x58);
		var searchKeyCount = MemoryUtil.Read<int>(requestArgumentsAddress + 0x14);
		var searchKeyData = requestArgumentsAddress + 0x1C;

		var isLanguageUpdated = false;
		var isQuestRewardsUpdated = false;
		var isQuestTypeUpdated = false;
		var isExpeditionObjectiveUpdated = false;
		var isRegionLevelUpdated = false;
		var isTargetMonsterUpdated = false;

		for(int i = 0; i < searchKeyCount; i++)
		{
			var keyID = MemoryUtil.Read<int>(searchKeyData - 0x4);
			var value = MemoryUtil.Read<int>(searchKeyData + 0x8);

			if(keyID == Constants.SEARCH_KEY_SEARCH_TYPE_ID)
			{
				CurrentSearchType = value switch
				{
					(int) SearchTypes.Session => SearchTypes.Session,
					(int) SearchTypes.Quest => SearchTypes.Quest,
					_ => SearchTypes.None
				};

				searchKeyData += 0x10;
				continue;
			}

			if(CurrentSearchType == SearchTypes.Session)
			{
				if(keyID == (int) SessionSearchKeyIDs.Language)
				{
					IsLanguageAny = false;
					isLanguageUpdated = true;
				}

				searchKeyData += 0x10;
				continue;
			}

			if(CurrentSearchType == SearchTypes.Quest)
			{
				if(keyID == (int) GuidingLandsSearchKeyIDs.IsGuidingLands)
				{
					if(value == (int) GuidingLands.Yes)
					{
						CurrentSearchType = SearchTypes.GuidingLands;
					}

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) QuestSearchKeyIDs.RewardsAvailable)
				{
					IsQuestRewardsNoPreference = false;
					isQuestRewardsUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) QuestSearchKeyIDs.Language)
				{
					IsLanguageAny = false;
					isLanguageUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) QuestSearchKeyIDs.QuestType)
				{
					IsQuestTypeNoPreference = false;
					isQuestTypeUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
			}

			if(CurrentSearchType == SearchTypes.GuidingLands)
			{
				if(keyID == (int) GuidingLandsSearchKeyIDs.ExpeditionObjective)
				{
					IsExpeditionObjectiveNoPreference = false;
					isExpeditionObjectiveUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) GuidingLandsSearchKeyIDs.RegionLevel)
				{
					IsRegionLevelNoPreference = false;
					isRegionLevelUpdated = true;

					searchKeyData += 0x10;
					continue;
				}

				if(keyID == (int) GuidingLandsSearchKeyIDs.TargetMonster)
				{
					IsTargetMonsterNoPreference = false;
					isTargetMonsterUpdated = true;

					searchKeyData += 0x10;
					continue;
				}
			}

			searchKeyData += 0x10;
		}

		if(!isLanguageUpdated) IsLanguageAny = true;
		if(!isQuestTypeUpdated) IsQuestTypeNoPreference = true;
		if(!isQuestRewardsUpdated) IsQuestRewardsNoPreference = true;
		if(!isExpeditionObjectiveUpdated) IsExpeditionObjectiveNoPreference = true;
		if(!isRegionLevelUpdated) IsRegionLevelNoPreference = true;
		if(!isTargetMonsterUpdated) IsTargetMonsterNoPreference = true;
	}

	private int OnStartRequest(nint netCore, nint netRequest)
	{
		try
		{
			// Phase Check
			var phase = MemoryUtil.Read<int>(netRequest + 0xE0);

			TeaLog.Info($"Search Request: Phase = {phase}");

			if(phase != 0)
			{
				CurrentSearchType = SearchTypes.None;
				return StartRequestHook!.Original(netCore, netRequest);
			}

			AnalyzeSearchKeys(netRequest);

			if(CurrentSearchType == SearchTypes.None) return StartRequestHook!.Original(netCore, netRequest);

			TeaLog.Info("");
			TeaLog.Info($"Search Request: {CurrentSearchType}");

			// Max Results

			ref var maxResultsRef = ref MemoryUtil.GetRef<int>(netRequest + 0x60);

			// Apply Stuff

			MaxSearchResultLimit_I.Apply(ref maxResultsRef);
			SteamRegionLockFix_I.Apply();
			SessionPlayerCountFilter_I.ApplyMin().ApplyMax();

			LanguageFilter_I.ApplyAnyLanguage();
			QuestTypeFilter_I.ApplyNoPreference();
			RewardFilter_I.ApplyNoPreference();

			ExpeditionObjectiveFilter_I.ApplyNoPreference();
			RegionLevelFilter_I.ApplyNoPreference();
			TargetMonsterFilter_I.ApplyNoPreference();
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("Core.OnStartRequest()", exception.ToString());
		}

		return StartRequestHook!.Original(netCore, netRequest);
	}

	private void OnNumericalFilter(nint steamInterface, nint keyAddress, int value, int comparison)
	{
		var skip = false;

		TeaLog.Debug("OnNumericalFilter");

		try
		{
			if(steamInterface != SteamMatchmakingInterface || CurrentSearchType == SearchTypes.None)
			{
				NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
				return;
			}

			var key = MemoryUtil.ReadString(keyAddress);

			TeaLog.Debug($"{key} ({GetSearchKeyName(key)}) {GetComparisonSign(comparison)} {value}");

			skip = PlayerTypeFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = QuestPreferenceFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = LanguageFilter_I.ApplySameLanguage(ref key, ref value, ref comparison) || skip;

			skip = QuestTypeFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = DifficultyFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = RewardFilter_I.ApplyRewardsAvailable(ref key, ref value, ref comparison) || skip;
			skip = TargetFilter_I.Apply(ref key, ref value, ref comparison) || skip;

			skip = ExpeditionObjectiveFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = RegionLevelFilter_I.Apply(ref key, ref value, ref comparison) || skip;
			skip = TargetMonsterFilter_I.Apply(ref key, ref value, ref comparison) || skip;
		}
		catch(Exception exception)
		{
			DebugManager_I.Report("Core.OnNumericalFilter()", exception.ToString());
		}

		if(!skip) NumericalFilterHook!.Original(steamInterface, keyAddress, value, comparison);
	}

	public void Dispose()
	{
		TeaLog.Info("Core: Disposing Hooks...");
		StartRequestHook?.Dispose();
		NumericalFilterHook?.Dispose();
	}
}
