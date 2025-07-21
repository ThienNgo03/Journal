namespace Journal.Events
{
    public class Controller
    {
    }
}

// yêu cầu người dùng chọn Exercise và sau đó tự nhập vào số rep, time, set 
//TH1:
//Exercise(Id, name, description, CreatedDate, LastUpdate)
//WorkoutLog(Id, ExId, Rep?, Time?, set, CreatedDate, LastUpdate)

//-------
//TH2:
//Exercise(Id, name, description, CreatedDate, LastUpdated)

//WorkoutLog(Id, WorkoutId, Rep?, Time?, Set, ExDate, CreatedDate, LastUpdated) //thực tế

//Workout(Id, ExId, UserId, CreatedDate, LastUpdated) //Dự định

//WeekPLan(Id, WorkoutId, DateOfWeek, Time, Rep?, HoldingTime?, Set) //Tập vào thứ mấy và mấy giờ



//Exercise(Id, name, description, CreatedDate, LastUpdated)
//WorkoutLog(Id, WorkoutId, Rep?, Time?, Set, ExDate, CreatedDate, LastUpdated) //thực tế
//Workout(Id, ExId, UserId, CreatedDate, LastUpdated) //Dự định
//WeekPLan(Id, WorkoutId, DateOfWeek, Time, Rep?, HoldingTime?, Set) //Tập vào thứ mấy và mấy giờ

//=======

//Game(type, maximum player)

//participants(info)

//Event(game, maximum player, time?, participants)
// Game-Event mqh 1-nhiều
// Event-Participants mqh nhiều-nhiều

//-------

//Game(name, maximum player)

//participants(info) 

//Event(gameId, participantsId, time?)
// Game-Event mqh 1-nhiều
// Event-Participants mqh nhiều-nhiều