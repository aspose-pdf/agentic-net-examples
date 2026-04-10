using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMetadataHelper
{
    /// <summary>
    /// Clears all XMP metadata from the specified PDF, preserving only the required PDF schema header.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
    public static void ClearXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Validate input arguments
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);

        // Use PdfXmpMetadata facade to manipulate XMP metadata.
        // The facade implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF document to the facade.
            xmp.BindPdf(inputPdfPath);

            // Remove all XMP entries. This leaves only the mandatory PDF schema header.
            xmp.Clear();

            // Save the modified PDF to the specified output path.
            xmp.Save(outputPdfPath);
        }
    }
}

// Minimal entry point required for a console‑style project.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration – uncomment and provide valid paths to run.
        // if (args.Length == 2)
        // {
        //     PdfMetadataHelper.ClearXmpMetadata(args[0], args[1]);
        // }
    }
}

/*
  ---------------------------------------------------------------------------
  Project file fix (AsposePdfApi.csproj)
  ---------------------------------------------------------------------------
  The original build error was caused by the SDK‑style project expecting a
  static Main entry point. Adding the minimal Program class with a Main method
  resolves CS5001. If the project is intended to be a library, you can also set
  <OutputType>Library</OutputType> in the .csproj, but providing a Main method
  is the simplest fix for the current configuration.
*/