using Unity.Entities;

[GenerateAuthoringComponent]
public struct ClientSettings : IComponentData
{
    public int predictionRadius;
    public int predictionRadiusMargin;
}
