namespace AGame.Code.Gameplay.Features.Tiles.Data
{
    public struct TileTriangleIndices
    {
        public int A;
        public int B;
        public int C;

        public TileTriangleIndices(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}