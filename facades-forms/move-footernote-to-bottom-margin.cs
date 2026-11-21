using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired margins (points)
                double leftMargin   = 20;
                double rightMargin  = 20;
                double bottomMargin = 20;
                double fieldHeight  = 15; // height of the FooterNote field

                // Calculate rectangle coordinates for the footer field
                double llx = leftMargin;
                double lly = bottomMargin;
                double urx = pageWidth - rightMargin;
                double ury = bottomMargin + fieldHeight;

                // Ensure the form and the specific field exist
                if (doc.Form != null && doc.Form["FooterNote"] != null)
                {
                    // Access the field (could be TextBoxField, CheckBoxField, etc.)
                    var field = doc.Form["FooterNote"];

                    // Set the field rectangle using a fully qualified Rectangle
                    field.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                }
            }

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"FooterNote repositioned and saved to '{outputPath}'.");
    }
}