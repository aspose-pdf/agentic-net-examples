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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            for (int i = 1; i <= doc.Form.Count; i++)
            {
                // Get the field; it may be of any type
                var field = doc.Form[i];

                // Process only signature fields
                if (field is SignatureField sigField)
                {
                    // Extract the signature image as a JPEG stream
                    using (Stream imgStream = sigField.ExtractImage())
                    {
                        if (imgStream != null)
                        {
                            // Build a unique file name using field name and page index
                            string fileName = $"{sigField.FullName}_Page{sigField.PageIndex}.jpg";
                            string outPath = Path.Combine(outputDir, fileName);

                            // Save the stream to a file
                            using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(file);
                            }

                            Console.WriteLine($"Extracted signature image to: {outPath}");
                        }
                        else
                        {
                            Console.WriteLine($"No image found for signature field: {sigField.FullName}");
                        }
                    }
                }
            }
        }
    }
}