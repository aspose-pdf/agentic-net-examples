using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationTests
{
    static void Main()
    {
        // Paths for the test files
        const string sourcePdf = "sample.pdf";
        const string modifiedPdf = "modified.pdf";

        // Create a PDF with a single text annotation
        CreateSamplePdf(sourcePdf);

        // Modify the Subject property of the annotation
        ModifyAnnotationSubject(sourcePdf, modifiedPdf, "Updated Subject");

        // Verify that the Subject was updated correctly
        bool testPassed = VerifyAnnotationSubject(modifiedPdf, "Updated Subject");
        Console.WriteLine(testPassed ? "Test Passed" : "Test Failed");
    }

    // Creates a PDF containing one TextAnnotation with an initial Subject value
    static void CreateSamplePdf(string outputPath)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the text annotation
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Sample Title",
                Subject  = "Original Subject",
                Contents = "This is a sample annotation."
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the PDF
            doc.Save(outputPath);
        }
    }

    // Updates the Subject property of the first TextAnnotation found in the PDF
    static void ModifyAnnotationSubject(string inputPath, string outputPath, string newSubject)
    {
        // Load the PDF, modify the annotation directly, and save.
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count == 0)
            {
                // No pages – just copy the source file.
                File.Copy(inputPath, outputPath, true);
                return;
            }

            Page firstPage = doc.Pages[1];
            if (firstPage.Annotations.Count == 0)
            {
                // No annotations – save the unchanged document.
                doc.Save(outputPath);
                return;
            }

            // The first annotation on the page should be the TextAnnotation we created.
            TextAnnotation textAnn = firstPage.Annotations[1] as TextAnnotation;
            if (textAnn != null)
            {
                textAnn.Subject = newSubject;
            }

            doc.Save(outputPath);
        }
    }

    // Checks whether the Subject of the first annotation matches the expected value
    static bool VerifyAnnotationSubject(string pdfPath, string expectedSubject)
    {
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has at least one page and one annotation
            if (doc.Pages.Count < 1) return false;
            Page firstPage = doc.Pages[1];
            if (firstPage.Annotations.Count < 1) return false;

            // Retrieve the annotation as a TextAnnotation
            TextAnnotation annotation = firstPage.Annotations[1] as TextAnnotation;
            if (annotation == null) return false;

            // Compare the Subject property
            return string.Equals(annotation.Subject, expectedSubject, StringComparison.Ordinal);
        }
    }
}
