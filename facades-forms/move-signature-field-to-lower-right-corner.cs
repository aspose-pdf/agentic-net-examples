using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Verify that the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Locate the form field whose partial name is "Signature"
            Aspose.Pdf.Forms.Field signatureField = null;
            foreach (Aspose.Pdf.Forms.Field f in doc.Form.Fields)
            {
                if (f.PartialName == "Signature")
                {
                    signatureField = f;
                    break;
                }
            }

            if (signatureField == null)
            {
                Console.Error.WriteLine("Form field 'Signature' not found.");
                return;
            }

            // The field must be a signature field to set its rectangle
            if (signatureField is Aspose.Pdf.Forms.SignatureField sigField)
            {
                // Desired dimensions (in points)
                const double fieldWidth   = 150; // width of the signature box
                const double fieldHeight  = 50;  // height of the signature box
                const double marginRight  = 20;  // distance from the right edge
                const double marginBottom = 20;  // distance from the bottom edge

                // Get page 5 dimensions
                Aspose.Pdf.Page page = doc.Pages[5];
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Calculate lower‑right rectangle coordinates
                double llx = pageWidth - marginRight - fieldWidth; // lower‑left X
                double lly = marginBottom;                        // lower‑left Y
                double urx = pageWidth - marginRight;              // upper‑right X
                double ury = marginBottom + fieldHeight;           // upper‑right Y

                // Assign the new rectangle to the signature field
                sigField.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }
            else
            {
                Console.Error.WriteLine("The field named 'Signature' is not a signature field.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field repositioned and saved to '{outputPath}'.");
    }
}