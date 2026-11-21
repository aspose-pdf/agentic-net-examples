using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade provides XFDF import/export

class XfdfCleanup
{
    /// <summary>
    /// Imports each XFDF file into the target PDF and deletes the XFDF file
    /// only if the import succeeded.
    /// </summary>
    /// <param name="pdfPath">Path to the target PDF file.</param>
    /// <param name="xfdfFiles">Array of temporary XFDF file paths.</param>
    public static void ImportAndClean(string pdfPath, string[] xfdfFiles)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {pdfPath}");
            return;
        }

        foreach (string xfdfPath in xfdfFiles)
        {
            if (string.IsNullOrWhiteSpace(xfdfPath) || !File.Exists(xfdfPath))
            {
                Console.Error.WriteLine($"XFDF file not found or invalid: {xfdfPath}");
                continue;
            }

            try
            {
                // Load the existing PDF document.
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Bind the document to the Form facade.
                    using (Form formFacade = new Form(pdfDoc))
                    {
                        // Import XFDF data from a stream (the facade expects a Stream, not a file path).
                        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                        {
                            formFacade.ImportXfdf(xfdfStream);
                        }

                        // Save the PDF back to the same path (overwrites with imported data).
                        formFacade.Save(pdfPath);
                    }
                }

                // Delete the temporary XFDF file only after a successful import.
                File.Delete(xfdfPath);
                Console.WriteLine($"Imported and deleted XFDF: {xfdfPath}");
            }
            catch (Exception ex)
            {
                // Log the error; the XFDF file is left untouched for troubleshooting.
                Console.Error.WriteLine($"Error importing XFDF '{xfdfPath}': {ex.Message}");
            }
        }
    }

    // Example usage
    static void Main()
    {
        string targetPdf = "output.pdf";
        string[] tempXfdfFiles = { "form1.xfdf", "form2.xfdf", "form3.xfdf" };

        ImportAndClean(targetPdf, tempXfdfFiles);
    }
}
