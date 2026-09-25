using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "doc1.pdf";
        const string secondPdf = "doc2.pdf";
        const string outputPdf = "comparison_result.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Configure side‑by‑side comparison options
            var options = new SideBySideComparisonOptions();

            // -----------------------------------------------------------------
            // Excluded areas – set via reflection to stay compatible with older
            // Aspose.Pdf versions where the properties may be missing.
            // -----------------------------------------------------------------
            SetExcludedArea(options, "ExcludedAreasFirstDocument", new Rectangle(100, 200, 300, 400));
            SetExcludedArea(options, "ExcludedAreasSecondDocument", new Rectangle(150, 250, 350, 450));

            // Perform visual side‑by‑side comparison. The method is static and
            // returns void; it writes the result directly to the supplied path.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPdf, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPdf}'.");
    }

    /// <summary>
    /// Adds a rectangle to the excluded‑area collection of a SideBySideComparisonOptions
    /// instance using reflection. This avoids compile‑time dependencies on properties
    /// that may be absent in some library versions.
    /// </summary>
    private static void SetExcludedArea(object optionsInstance, string propertyName, Rectangle rect)
    {
        var propInfo = optionsInstance.GetType().GetProperty(propertyName);
        if (propInfo == null)
            return; // Property not available – nothing to set.

        // The property is expected to be IList<Rectangle>. Create a List<Rectangle>,
        // add the rectangle, and assign it.
        var listType = typeof(List<>).MakeGenericType(typeof(Rectangle));
        var listInstance = (IList)Activator.CreateInstance(listType);
        listInstance.Add(rect);
        propInfo.SetValue(optionsInstance, listInstance);
    }
}
