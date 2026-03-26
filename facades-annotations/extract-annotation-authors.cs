using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        // Determine total number of pages in the PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Create an array containing all possible annotation types
        AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

        // Extract annotations and group them by author (stored in Subject of MarkupAnnotation)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            IList<Annotation> annotationList = editor.ExtractAnnotations(1, pageCount, allTypes);

            var authorGroups = new Dictionary<string, List<Annotation>>(StringComparer.OrdinalIgnoreCase);

            foreach (Annotation annotation in annotationList)
            {
                // Only markup annotations have the Subject (author) property.
                string author = "Unknown";
                if (annotation is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Subject))
                {
                    author = markup.Subject;
                }

                if (!authorGroups.ContainsKey(author))
                {
                    authorGroups[author] = new List<Annotation>();
                }
                authorGroups[author].Add(annotation);
            }

            Console.WriteLine("Annotation author report:");
            foreach (KeyValuePair<string, List<Annotation>> pair in authorGroups)
            {
                Console.WriteLine($"Author: {pair.Key} - Count: {pair.Value.Count}");
            }
        }
    }
}
