using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Configuration: when true, read‑only annotations are preserved and NOT flattened.
    private const bool SkipFlattenReadOnly = true;

    static void Main()
    {
        const string inputFolder = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document
            Document doc = new Document(inputPath);

            // -----------------------------------------------------------------
            // 1. Collect read‑only annotations so they can be re‑added later.
            // -----------------------------------------------------------------
            var readOnlyAnnotations = new List<(int pageNumber, Annotation annotation)>();

            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                // Iterate backwards because we may delete annotations while iterating.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation annot = page.Annotations[i];
                    bool isReadOnly = IsAnnotationReadOnly(annot);

                    if (isReadOnly)
                    {
                        // Store a clone so we can re‑add it after flattening.
                        readOnlyAnnotations.Add((pageIdx, (Annotation)annot.Clone()));
                        // Remove it now – we do NOT want it to be flattened.
                        page.Annotations.Delete(i);
                    }
                }
            }

            // -----------------------------------------------------------------
            // 2. Flatten the remaining (non‑read‑only) annotations.
            // -----------------------------------------------------------------
            // Use PdfAnnotationEditor.FlatteningAnnotations() – the Document class does not expose a flatten method.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                editor.FlatteningAnnotations();
            }

            // -----------------------------------------------------------------
            // 3. Re‑add the read‑only annotations if the configuration says to keep them.
            // -----------------------------------------------------------------
            if (SkipFlattenReadOnly)
            {
                foreach (var (pageNumber, annotation) in readOnlyAnnotations)
                {
                    doc.Pages[pageNumber].Annotations.Add(annotation);
                }
            }

            // -----------------------------------------------------------------
            // 4. Save the processed PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPath);

            Console.WriteLine($"Processed (read‑only {(SkipFlattenReadOnly ? "preserved" : "flattened")}): {fileName}");
        }
    }

    /// <summary>
    /// Determines whether an annotation is marked as read‑only.
    /// Handles both the explicit "ReadOnly" property (newer versions) and the Flags enum.
    /// </summary>
    private static bool IsAnnotationReadOnly(Annotation annot)
    {
        // 1. Try the explicit ReadOnly property if it exists.
        var readOnlyProp = annot.GetType().GetProperty("ReadOnly");
        if (readOnlyProp != null && readOnlyProp.PropertyType == typeof(bool))
        {
            return (bool?)readOnlyProp.GetValue(annot) == true;
        }

        // 2. Fallback – inspect the Flags enum for the ReadOnly flag.
        var flagsProp = annot.GetType().GetProperty("Flags");
        if (flagsProp != null)
        {
            object flagsValue = flagsProp.GetValue(annot);
            if (flagsValue != null)
            {
                Type enumType = flagsProp.PropertyType;
                // Try to parse the enum member named "ReadOnly" safely.
                if (Enum.TryParse(enumType, "ReadOnly", out object readOnlyEnum))
                {
                    int flagsInt = Convert.ToInt32(flagsValue);
                    int readOnlyInt = Convert.ToInt32(readOnlyEnum);
                    return (flagsInt & readOnlyInt) != 0;
                }
            }
        }

        return false;
    }
}
