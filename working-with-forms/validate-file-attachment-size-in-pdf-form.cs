using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";
        const string outputPdfPath = "form_checked.pdf";
        const long maxFileSizeBytes = 5L * 1024 * 1024; // 5 MB

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Optional safety net – limit memory loading to 5 MB
        Document.FileSizeLimitToMemoryLoading = 5; // value is in megabytes

        // Load the PDF form
        using (Document doc = new Document(inputPdfPath))
        {
            // Locate the file‑select box field (adjust the field name as needed)
            const string fileFieldName = "fileSelect";
            FileSelectBoxField fileField = doc.Form[fileFieldName] as FileSelectBoxField;

            if (fileField == null)
            {
                Console.Error.WriteLine($"FileSelectBoxField '{fileFieldName}' not found.");
                doc.Save(outputPdfPath);
                return;
            }

            // The field value holds the selected file path (may be empty)
            string selectedFilePath = fileField.Value?.ToString();

            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                Console.WriteLine("No file selected or file does not exist. Form can be submitted.");
                doc.Save(outputPdfPath);
                return;
            }

            long selectedFileSize = new FileInfo(selectedFilePath).Length;

            if (selectedFileSize > maxFileSizeBytes)
            {
                // Add a warning annotation to inform the user
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle warningRect = new Aspose.Pdf.Rectangle(100, 700, 500, 750);
                TextAnnotation warning = new TextAnnotation(firstPage, warningRect)
                {
                    Title = "File Too Large",
                    Contents = $"Selected file size ({selectedFileSize / (1024 * 1024)} MB) exceeds the 5 MB limit.",
                    Color = Aspose.Pdf.Color.Red,
                    Open = true,
                    Icon = TextIcon.Note
                };
                firstPage.Annotations.Add(warning);

                Console.WriteLine("File size exceeds limit; submission blocked.");
            }
            else
            {
                // Add a submit button (or ensure one exists) to allow form submission
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 600, 200, 630);
                ButtonField submitBtn = new ButtonField(doc, btnRect)
                {
                    PartialName = "submitBtn",
                    NormalCaption = "Submit"
                };

                // Create a SubmitFormAction that posts the form to a URL
                SubmitFormAction submitAction = new SubmitFormAction();
                submitAction.Url = new FileSpecification("https://example.com/submit");
                // Assign the action to the button's activation event
                submitBtn.OnActivated = submitAction;

                // Add the button to the form on the first page
                doc.Form.Add(submitBtn, 1);

                Console.WriteLine("File size within limit; submission enabled.");
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}
