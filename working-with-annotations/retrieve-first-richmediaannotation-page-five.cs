using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page page = doc.Pages[5];

            // LINQ query to get the first RichMediaAnnotation on page 5
            RichMediaAnnotation richMedia = (from ann in page.Annotations
                                             where ann is RichMediaAnnotation
                                             select (RichMediaAnnotation)ann).FirstOrDefault();

            if (richMedia == null)
            {
                Console.WriteLine("No RichMediaAnnotation found on page 5.");
                return;
            }

            // Display selected properties of the RichMediaAnnotation
            Console.WriteLine("RichMediaAnnotation found:");
            Console.WriteLine($"  Name               : {richMedia.Name}");
            Console.WriteLine($"  AnnotationType     : {richMedia.AnnotationType}");
            Console.WriteLine($"  Rect               : {richMedia.Rect}");

            // RichMediaContent and RichMediaSettings may not exist in older library versions.
            // Use reflection to safely access them if they are present.
            PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
            PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");

            bool hasContent = contentProp != null && contentProp.GetValue(richMedia) != null;
            bool hasSettings = settingsProp != null && settingsProp.GetValue(richMedia) != null;

            Console.WriteLine($"  RichMediaContent   : {(hasContent ? "Present" : "None")}");
            Console.WriteLine($"  RichMediaSettings  : {(hasSettings ? "Present" : "None")}");
        }
    }
}
