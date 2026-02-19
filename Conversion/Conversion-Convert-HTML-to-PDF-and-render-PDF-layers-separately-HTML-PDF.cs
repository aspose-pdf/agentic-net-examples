using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;

class HtmlToPdfWithLayers
{
    static void Main(string[] args)
    {
        // Input HTML file and output PDF file
        string htmlPath = "input.html";
        string pdfPath = "output.pdf";

        // Verify that the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Convert HTML to PDF
            // -----------------------------------------------------------------
            // Load the HTML document using HtmlLoadOptions (no base path needed)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Create a new PDF document from the HTML source
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");

            // -----------------------------------------------------------------
            // 2. Render each PDF layer (Optional Content Group) separately
            // -----------------------------------------------------------------
            // The OptionalContentGroups API is not available in older Aspose.Pdf versions.
            // Use reflection to determine whether the property exists at runtime.
            var ocgsProp = pdfDocument.GetType().GetProperty("OptionalContentGroups");
            if (ocgsProp == null)
            {
                Console.WriteLine("Optional Content Groups (layers) are not supported in this Aspose.Pdf version.");
                return;
            }

            var ocgs = ocgsProp.GetValue(pdfDocument) as IEnumerable;
            if (ocgs == null)
            {
                Console.WriteLine("No PDF layers (Optional Content Groups) were found.");
                return;
            }

            // Create a directory to hold the per‑layer PDFs
            string layersDir = "Layers";
            Directory.CreateDirectory(layersDir);

            foreach (var layer in ocgs)
            {
                // Clone the original document so changes to visibility do not affect other iterations
                Document layerDoc = new Document(pdfPath);

                // Hide all layers in the cloned document
                var groupsProp = layerDoc.GetType().GetProperty("OptionalContentGroups");
                var groups = groupsProp?.GetValue(layerDoc) as IEnumerable;

                // Prepare a variable to hold the current layer name
                string layerName = null;

                if (groups != null)
                {
                    foreach (var grp in groups)
                    {
                        var visibleProp = grp.GetType().GetProperty("Visible");
                        visibleProp?.SetValue(grp, false);
                    }

                    // Make the current layer visible
                    var layerNameProp = layer.GetType().GetProperty("Name");
                    layerName = layerNameProp?.GetValue(layer) as string;

                    foreach (var grp in groups)
                    {
                        var nameProp = grp.GetType().GetProperty("Name");
                        var grpName = nameProp?.GetValue(grp) as string;
                        if (grpName == layerName)
                        {
                            var visibleProp = grp.GetType().GetProperty("Visible");
                            visibleProp?.SetValue(grp, true);
                            break;
                        }
                    }
                }

                // Build a safe file name for this layer
                var layerNameForFile = layerName ?? "UnnamedLayer";
                string safeLayerName = string.IsNullOrWhiteSpace(layerNameForFile) ? "UnnamedLayer" : layerNameForFile;
                foreach (char c in Path.GetInvalidFileNameChars())
                    safeLayerName = safeLayerName.Replace(c, '_');

                string layerPdfPath = Path.Combine(layersDir, $"Layer_{safeLayerName}.pdf");

                // Save the PDF that contains only this layer
                layerDoc.Save(layerPdfPath);

                Console.WriteLine($"Layer '{layerNameForFile}' saved to: {layerPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
