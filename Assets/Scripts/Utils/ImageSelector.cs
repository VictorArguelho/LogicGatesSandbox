using SFB;

public static class ImageSelector
{
    public static string SelectImage()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "Selecionar imagem",
            "",
            new[]
            {
                new ExtensionFilter("Imagens", "png", "jpg", "jpeg")
            },
            false
        );

        return paths.Length > 0 ? paths[0] : null;
    }
}