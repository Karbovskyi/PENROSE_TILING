namespace AGame.Code.Gameplay.Features.Tiles.Data
{
    public struct SnapLinkData
    {
        public readonly int SourceVertexId;
        public readonly int SourceVertexIndex;
        public readonly int TargetVertexId;

        public SnapLinkData(int sourceVertexId, int sourceVertexIndex, int targetVertexId)
        {
            SourceVertexId = sourceVertexId;
            SourceVertexIndex = sourceVertexIndex;
            TargetVertexId = targetVertexId;
        }
    }
}