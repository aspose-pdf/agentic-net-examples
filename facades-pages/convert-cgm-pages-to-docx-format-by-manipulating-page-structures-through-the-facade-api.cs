using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Resolve file paths to absolute paths and verify that the source file exists.
        //    This prevents Aspose.Pdf from trying to interpret a relative path as a URI,
        //    which caused the System.UriFormatException in the original code.
        // ---------------------------------------------------------------------
        string cgmPath   = Path.GetFullPath("input.cgm");   // CGM source file (absolute path)
        string docxPath  = Path.GetFullPath("output.docx"); // Desired DOCX output (absolute path)

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM source file not found – '{cgmPath}'.");
            return;
        }

        // ---------------------------------------------------------------------
        // 2. Convert CGM → PDF using the PdfProducer facade. The PDF is kept in a
        //    memory stream to avoid creating an intermediate file on disk.
        // ---------------------------------------------------------------------
        using (MemoryStream pdfStream = new MemoryStream())
        {
            // Produce PDF from CGM. ImportFormat.Cgm specifies the source format.
            // The method expects a file system path, not a URI, therefore we pass the
            // absolute path resolved above.
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfStream);

            // Reset the stream position before loading it into a Document instance.
            pdfStream.Position = 0;

            // -----------------------------------------------------------------
            // 3. Load the generated PDF into a Document object. Wrap it in a using
            //    block for deterministic disposal of native resources.
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfStream))
            {
                // -----------------------------------------------------------------
                // 4. OPTIONAL: Manipulate page structures via the PdfPageEditor facade.
                //    In this example we enforce a uniform A4 page size and clear any
                //    rotation that might have been introduced during conversion.
                // -----------------------------------------------------------------
                PdfPageEditor editor = new PdfPageEditor(pdfDoc);
                foreach (Page page in pdfDoc.Pages)
                {
                    // Set page size to A4 (595 x 842 points) – adjust if required.
                    editor.PageSize = new PageSize(595, 842);
                    // Reset any rotation.
                    editor.Rotation = 0;
                }
                // Apply the changes made by the editor.
                editor.ApplyChanges();

                // -----------------------------------------------------------------
                // 5. Save the PDF as DOCX using DocSaveOptions.
                // -----------------------------------------------------------------
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Output format: DOCX.
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use flow recognition mode for better layout preservation.
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional but often useful).
                    RecognizeBullets = true
                };

                // Save the document; the overload with SaveOptions ensures DOCX output.
                pdfDoc.Save(docxPath, saveOptions);
            }
        }

        Console.WriteLine($"CGM has been successfully converted to DOCX: '{docxPath}'.");
    }
}
