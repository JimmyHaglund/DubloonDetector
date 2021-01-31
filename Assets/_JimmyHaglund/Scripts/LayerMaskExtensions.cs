namespace JimmyHaglund {
    public static class LayerMaskExtensions {
        public static bool LayerMaskContains(int layerMask, int value) {
            return layerMask == (layerMask | 1 << value);
        }
    }
}