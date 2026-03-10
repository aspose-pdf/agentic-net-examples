using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for some advanced features (optional)

class ZugFeRdProcessor
{
    static void Main()
    {
        const string inputPdfPath = "invoice_with_zugferd.pdf";
        const string outputFolder = "ExtractedZugFeRd";
        const string newZugFeRdXmlPath = "new_zugferd.xml"; // optional replacement file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Extract existing ZUGFeRD XML files that are embedded in the PDF.
            // -----------------------------------------------------------------
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                // Identify ZUGFeRD files by extension or by name containing "ZUGFeRD".
                string fileName = fileSpec.Name ?? string.Empty;
                if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) ||
                    fileName.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string extractedPath = Path.Combine(outputFolder, fileName);
                    using (FileStream outStream = new FileStream(extractedPath, FileMode.Create, FileAccess.Write))
                    {
                        // The Contents property returns a Stream with the embedded data.
                        fileSpec.Contents.CopyTo(outStream);
                    }
                    Console.WriteLine($"Extracted ZUGFeRD file: {extractedPath}");
                }
            }

            // -----------------------------------------------------------------
            // 2. OPTIONAL: Replace the existing ZUGFeRD XML with a new one.
            // -----------------------------------------------------------------
            if (File.Exists(newZugFeRdXmlPath))
            {
                // Remove any existing ZUGFeRD XML files first.
                // The Delete method expects the *name* of the embedded file, not an index.
                for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    FileSpecification existingSpec = pdfDoc.EmbeddedFiles[i];
                    string name = existingSpec.Name ?? string.Empty;
                    if (name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) ||
                        name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        pdfDoc.EmbeddedFiles.Delete(name);
                    }
                }

                // Add the new ZUGFeRD XML as an embedded file.
                using (FileStream xmlStream = new FileStream(newZugFeRdXmlPath, FileMode.Open, FileAccess.Read))
                {
                    // Create a FileSpecification – the constructor takes the file path and a description.
                    var newSpec = new FileSpecification(newZugFeRdXmlPath, Path.GetFileName(newZugFeRdXmlPath));
                    // Assign the stream containing the XML data.
                    newSpec.Contents = xmlStream;
                    pdfDoc.EmbeddedFiles.Add(newSpec);
                }

                // Save the modified PDF (creates a new file to keep the original untouched).
                const string modifiedPdfPath = "invoice_with_updated_zugferd.pdf";
                pdfDoc.Save(modifiedPdfPath);
                Console.WriteLine($"Modified PDF saved with new ZUGFeRD XML: {modifiedPdfPath}");
            }
        }
    }
}
