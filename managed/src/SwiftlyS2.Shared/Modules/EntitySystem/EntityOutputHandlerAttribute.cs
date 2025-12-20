namespace SwiftlyS2.Shared.EntitySystem;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EntityOutputHandler : Attribute
{
    public string DesignerName { get; set; }
    public string OutputName { get; set; }

    public EntityOutputHandler(string designerName, string outputName)
    {
        DesignerName = designerName;
        OutputName = outputName;
    }
}