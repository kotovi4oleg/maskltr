using System.Data;

namespace MLAccessCore.Connections {
    public interface IConnection {
        IDbConnection Connection { get; }
    }
}