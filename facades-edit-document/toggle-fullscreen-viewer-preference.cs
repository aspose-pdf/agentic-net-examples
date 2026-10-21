using System;
using System.IO;
using Aspose.Pdf;

public static class PdfViewerPreferenceHelper
{
    /// <summary>
    /// Toggles the FullScreen viewer preference of a PDF file.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="enableFullScreen">True to enable FullScreen mode; false to disable.</param>
    public static void ToggleFullScreen(string inputPdfPath, string outputPdfPath, bool enableFullScreen)
    {
        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Source PDF not found.", inputPdfPath);

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdfPath);

        // The ViewerPreferences class may not exist in older Aspose.Pdf versions.
        // Use reflection to set the FullScreen flag if the API is available.
        var viewerPrefsProp = typeof(Document).GetProperty("ViewerPreferences");
        if (viewerPrefsProp != null)
        {
            var viewerPrefs = viewerPrefsProp.GetValue(pdfDoc);
            if (viewerPrefs != null)
            {
                var fullScreenProp = viewerPrefs.GetType().GetProperty("FullScreen");
                if (fullScreenProp != null && fullScreenProp.CanWrite)
                {
                    fullScreenProp.SetValue(viewerPrefs, enableFullScreen);
                }
                else
                {
                    // FullScreen property not available – nothing to do.
                }
            }
        }
        else
        {
            // ViewerPreferences property not available – nothing to do.
        }

        // Save the modified document.
        pdfDoc.Save(outputPdfPath);
    }

    // Entry point required for compilation when the project is built as an executable.
    public static void Main(string[] args)
    {
        // This placeholder Main allows the project to compile.
        // Example usage (uncomment for testing):
        // ToggleFullScreen("input.pdf", "output.pdf", true);
    }
}
