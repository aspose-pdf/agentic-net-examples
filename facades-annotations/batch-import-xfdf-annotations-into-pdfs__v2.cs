using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Wrapper that uses the Form facade to import XFDF data.
public class PdfXfdfForm : IDisposable
{
    private Document? _document;
    private Form? _formFacade;

    // Binds the PDF file to the wrapper and prepares the Form facade.
    public void BindPdf(string pdfPath)
    {
        _document = new Document(pdfPath);
        _formFacade = new Form();
        _formFacade.BindPdf(_document);
    }

    // Imports XFDF data into the PDF's form fields using the facade.
    public void ImportXfdf(string xfdfPath)
    {
        if (_formFacade == null)
            throw new InvalidOperationException("PDF has not been bound. Call BindPdf first.");

        // The ImportXfdf overload expects a Stream in recent Aspose.Pdf versions.
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            _formFacade.ImportXfdf(xfdfStream);
        }
    }

    // Saves the modified PDF to the specified path.
    public void Save(string outputPath)
    {
        _document?.Save(outputPath);
    }

    // Clean up unmanaged resources.
    public void Dispose()
    {
        _formFacade?.Dispose();
        _document?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        // Folder containing PDFs and corresponding XFDF files
        const string inputFolder = "BatchInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = "BatchOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Determine base file name without extension
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            // Expected XFDF file with the same base name
            string xfdfPath = Path.Combine(inputFolder, baseName + ".xfdf");

            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No matching XFDF for '{baseName}.pdf' – skipping.");
                continue;
            }

            try
            {
                using (PdfXfdfForm xfdfForm = new PdfXfdfForm())
                {
                    xfdfForm.BindPdf(pdfPath);
                    xfdfForm.ImportXfdf(xfdfPath);

                    string outputPdfPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    xfdfForm.Save(outputPdfPath);

                    Console.WriteLine($"Imported XFDF into PDF: {outputPdfPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch XFDF import completed.");
    }
}