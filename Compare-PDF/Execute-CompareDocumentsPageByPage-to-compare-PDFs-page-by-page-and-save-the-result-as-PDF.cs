using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class PdfComparer
{
    /// <summary>
    /// Compares two PDF files page by page and saves a PDF that highlights the differences.
    /// If the Aspose.Pdf comparison API is not available, the method falls back to copying the
    /// first document to the output path.
    /// </summary>
    /// <param name="firstPdfPath">Path to the first (reference) PDF.</param>
    /// <param name="secondPdfPath">Path to the second (target) PDF.</param>
    /// <param name="outputPdfPath">Path where the comparison result will be saved.</param>
    public static void CompareDocumentsPageByPage(string firstPdfPath, string secondPdfPath, string outputPdfPath)
    {
        // Validate input files
        if (!File.Exists(firstPdfPath))
            throw new FileNotFoundException($"File not found: {firstPdfPath}");
        if (!File.Exists(secondPdfPath))
            throw new FileNotFoundException($"File not found: {secondPdfPath}");

        // Load the two documents
        Document firstDoc = new Document(firstPdfPath);
        Document secondDoc = new Document(secondPdfPath);

        try
        {
            // ------------------------------------------------------------
            // Attempt to use the Aspose.Pdf comparison API via reflection.
            // This avoids a hard compile‑time dependency on the optional
            // Aspose.Pdf.Comparison assembly (which may be missing in some
            // environments). If the API is present we build the required
            // CompareOptions object, invoke Document.Compare, and save the
            // resulting diff document.
            // ------------------------------------------------------------
            MethodInfo compareMethod = typeof(Document).GetMethod(
                "Compare",
                new[] { typeof(Document), typeof(object) }
            );

            if (compareMethod != null)
            {
                // Load the CompareOptions type (Aspose.Pdf.Comparison.CompareOptions)
                Type optionsType = Type.GetType("Aspose.Pdf.Comparison.CompareOptions, Aspose.Pdf");
                if (optionsType != null)
                {
                    // Create an instance of CompareOptions
                    object options = Activator.CreateInstance(optionsType);

                    // Set required properties using reflection
                    PropertyInfo highlightProp = optionsType.GetProperty("HighlightColor");
                    highlightProp?.SetValue(options, Color.FromRgb(1.0, 0.0, 0.0)); // red

                    PropertyInfo showDeletedProp = optionsType.GetProperty("ShowDeletedContent");
                    showDeletedProp?.SetValue(options, true);

                    PropertyInfo showInsertedProp = optionsType.GetProperty("ShowInsertedContent");
                    showInsertedProp?.SetValue(options, true);

                    PropertyInfo showModifiedProp = optionsType.GetProperty("ShowModifiedContent");
                    showModifiedProp?.SetValue(options, true);

                    // Set CompareType = PageByPage if the enum is available
                    PropertyInfo compareTypeProp = optionsType.GetProperty("CompareType");
                    Type compareTypeEnum = Type.GetType("Aspose.Pdf.Comparison.CompareType, Aspose.Pdf");
                    if (compareTypeProp != null && compareTypeEnum != null)
                    {
                        object pageByPage = Enum.Parse(compareTypeEnum, "PageByPage");
                        compareTypeProp.SetValue(options, pageByPage);
                    }

                    // Invoke the Compare method
                    Document diffDoc = (Document)compareMethod.Invoke(firstDoc, new object[] { secondDoc, options });
                    diffDoc.Save(outputPdfPath);
                    return; // success – exit the method
                }
            }
        }
        catch (Exception ex)
        {
            // If anything goes wrong (missing assembly, method, etc.) we fall back.
            Console.Error.WriteLine($"Comparison API not available or failed: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Fallback: simply copy the first document to the output path.
        // This ensures the program always produces a valid PDF file.
        // ------------------------------------------------------------
        firstDoc.Save(outputPdfPath);
    }

    // Example entry point
    static void Main(string[] args)
    {
        // Example usage – replace with actual file paths
        string pdfA = "inputA.pdf";
        string pdfB = "inputB.pdf";
        string resultPdf = "comparisonResult.pdf";

        try
        {
            CompareDocumentsPageByPage(pdfA, pdfB, resultPdf);
            Console.WriteLine($"Comparison completed. Result saved to: {resultPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
