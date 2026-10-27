using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImages
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputDir = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Guard against documents without a form
            if (doc.Form == null || doc.Form.Fields == null)
                return;

            int sigCounter = 0;
            // Iterate through all signature fields in the document
            foreach (SignatureField sigField in doc.Form.Fields.OfType<SignatureField>())
            {
                // The appearance of a signature field can be extracted directly
                // using the ExtractImage() method which returns a stream containing
                // the rendered appearance (usually a bitmap).
                using (Stream appearanceStream = sigField.ExtractImage())
                {
                    if (appearanceStream == null || appearanceStream.Length == 0)
                        continue; // No visual appearance to extract

                    // Determine a safe file extension (PNG works for most cases)
                    string extension = GetExtension(null); // default to .png
                    string outPath = Path.Combine(outputDir,
                        $"Signature_{sigCounter}{extension}");

                    // Save the stream to a file
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        appearanceStream.CopyTo(fs);
                    }

                    Console.WriteLine($"Extracted signature appearance to: {outPath}");
                    sigCounter++;
                }
            }
        }
    }

    // Returns a default image file extension. In older Aspose.Pdf versions the
    // ImageInfo property is not available, so we avoid that dependency.
    static string GetExtension(XImage img)
    {
        // The method signature matches the verified fix pattern; the parameter is
        // kept for compatibility but is not used because we default to PNG.
        return ".png";
    }
}
