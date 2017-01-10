using System.Collections.ObjectModel;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IProcessActivity<T1, T2, T3> : IInterval
        where T1 : IProcessDayActivity<T2, T3>
        where T2 : IProcessWork<T3>
        where T3 : IGroundWorkState
    {
        T1 LinkProcess { get; set; }
        ObservableCollection<T3> GroundWorkStates { get; set; }
    }
}