using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public class Program
{
    /// <summary>
    /// Returns a dictionary that maps each annotation type present in the PDF to the number of its occurrences.
    /// If the input file does not exist, a minimal placeholder PDF is created automatically.
    /// </summary>
    public static Dictionary<Aspose.Pdf.Annotations.AnnotationType, int> CountAnnotationTypes(string pdfPath)
    {
        // Ensure the file exists – create a minimal placeholder PDF if it does not.
        if (!File.Exists(pdfPath))
        {
            // Create a one‑page PDF with no content.
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(pdfPath);
        }

        using (Document doc = new Document(pdfPath))
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                int startPage = 1;
                int endPage = doc.Pages.Count;
                Aspose.Pdf.Annotations.AnnotationType[] allTypes = (Aspose.Pdf.Annotations.AnnotationType[])Enum.GetValues(typeof(Aspose.Pdf.Annotations.AnnotationType));
                IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, allTypes);

                var counts = new Dictionary<Aspose.Pdf.Annotations.AnnotationType, int>();
                foreach (Annotation annotation in annotations)
                {
                    Aspose.Pdf.Annotations.AnnotationType type = annotation.AnnotationType;
                    if (counts.ContainsKey(type))
                    {
                        counts[type] = counts[type] + 1;
                    }
                    else
                    {
                        counts[type] = 1;
                    }
                }
                return counts;
            }
        }
    }

    public static void Main()
    {
        const string inputPath = "input.pdf";
        Dictionary<Aspose.Pdf.Annotations.AnnotationType, int> annotationCounts = CountAnnotationTypes(inputPath);
        foreach (KeyValuePair<Aspose.Pdf.Annotations.AnnotationType, int> entry in annotationCounts)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}
