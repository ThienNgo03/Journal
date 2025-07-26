namespace Journal.Competition.Delete.Messager;

public record Message(Guid Id, bool DeleteSoloPool, bool DeleteTeamPool);
