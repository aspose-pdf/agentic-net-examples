using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputFolder = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page (1‑based indexing)
                for (int idx = 1; idx <= page.Annotations.Count; idx++)
                {
                    Annotation annotation = page.Annotations[idx];

                    // Check if the annotation is a signature field
                    if (annotation is SignatureField signatureField)
                    {
                        // Extract the visual appearance as a JPEG stream
                        using (Stream imageStream = signatureField.ExtractImage())
                        {
                            if (imageStream != null)
                            {
                                // Build a unique file name for the extracted image
                                string fileName = $"Signature_{signatureField.Name}_Page{page.Number}.jpg";
                                string outputPath = Path.Combine(outputFolder, fileName);

                                // Save the stream to a file
                                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                                {
                                    imageStream.CopyTo(file);
                                }

                                Console.WriteLine($"Extracted signature image saved to: {outputPath}");
                            }
                            else
                            {
                                Console.WriteLine($"No image found for signature field '{signatureField.Name}' on page {page.Number}.");
                            }
                        }
                    }
                }
            }
        }
    }
}