namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBaseModelEntity
{
    /// <summary>
    /// Sets the model to the entity.
    /// </summary>
    /// <param name="model">The model path to be used.</param>
    public void SetModel(string model);

    /// <summary>
    /// Sets the model to the entity asynchronously.
    /// </summary>
    /// <param name="model">The model path to be used.</param>
    public Task SetModelAsync(string model);

    /// <summary>
    /// Sets the bodygroup to the entity.
    /// </summary>
    public void SetBodygroupByName(string group, int value);

    /// <summary>
    /// Sets the bodygroup to the entity asynchronously.
    /// </summary>
    /// <param name="group">The name of the bodygroup to be set.</param>
    /// <param name="value">The value to be set for the bodygroup.</param>
    public Task SetBodygroupByNameAsync(string group, int value);

    /// <summary>
    /// Sets the scale of the entity.
    /// </summary>
    public void SetScale(float scale);

}