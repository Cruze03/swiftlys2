using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Datamaps;

public interface IDatamapFunction<T> where T : ISchemaClass<T>
{

    public IDatamapFunctionOperator<T> Get(string pluginId);

}