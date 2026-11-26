using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for SimpleFontSubstitution and FontRepository

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdf = "output_pdfa3a.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // ------------------------------------------------------------
                // 1. Embed the external file as an embedded file (PDF portfolio)
                // ------------------------------------------------------------
                var fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath))
                {
                    // The Contents stream must be supplied explicitly; otherwise the file
                    // is only referenced by path which is not allowed for PDF/A.
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                };
                doc.EmbeddedFiles.Add(fileSpec);

                // ------------------------------------------------------------
                // 2. Configure simple font substitution (global repository)
                // ------------------------------------------------------------
                // Replace any missing font with Arial.
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

                // ------------------------------------------------------------
                // 3. Convert the document to PDF/A‑3a
                // ------------------------------------------------------------
                var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3A);
                doc.Convert(convOptions);

                // ------------------------------------------------------------
                // 4. Save the converted PDF/A‑3a document
                // ------------------------------------------------------------
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A-3a file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
