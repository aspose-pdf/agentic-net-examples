using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for Annotation and ExtractImage()
using Aspose.Pdf.Forms;        // for SignatureField and field name handling

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputFolder = "SignatureImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document – wrapped in a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Look for signature fields (SignatureField derives from FormField -> Annotation)
                    if (annotation is SignatureField signatureField)
                    {
                        // Try to extract the visual appearance as a JPEG stream
                        // (ExtractImage returns null if no image is present)
                        using (Stream imageStream = signatureField.ExtractImage())
                        {
                            if (imageStream != null)
                            {
                                // Build a file name using the field's partial name (or a fallback)
                                string fieldName = !string.IsNullOrEmpty(signatureField.PartialName)
                                                   ? signatureField.PartialName
                                                   : $"Signature_{pageIndex}";
                                string outputPath = Path.Combine(outputFolder, $"{fieldName}.jpg");

                                // Save the JPEG stream to disk
                                using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                                {
                                    imageStream.CopyTo(fileOut);
                                }

                                Console.WriteLine($"Extracted signature image saved to: {outputPath}");
                            }
                            else
                            {
                                Console.WriteLine($"No image found for signature field '{signatureField.PartialName}' on page {pageIndex}.");
                            }
                        }
                    }
                }
            }
        }
    }
}
