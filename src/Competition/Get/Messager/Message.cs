namespace Journal.Competition.Get.Messager;

public record Message(Guid Id, bool IncludeSoloPool, bool IncludeTeamPool);
