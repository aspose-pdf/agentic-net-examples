using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfPipelineExample
{
    // Define the types of actions the pipeline can perform
    enum ActionType
    {
        AddAttachment,
        SetViewerPreference,
        CreateFileAnnotation
    }

    // Simple configuration model for a pipeline step
    class PipelineStep
    {
        public ActionType Action { get; set; }

        // Parameters for AddAttachment
        public string AttachmentPath { get; set; }
        public string AttachmentDescription { get; set; }

        // Parameters for SetViewerPreference
        public int ViewerPreference { get; set; }

        // Parameters for CreateFileAnnotation
        public System.Drawing.Rectangle AnnotationRect { get; set; }
        public string AnnotationContents { get; set; }
        public string AnnotationFilePath { get; set; }
        public int AnnotationPage { get; set; }
        public string AnnotationIconName { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputPdf = "output.pdf";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // Build a simple in‑code configuration describing the pipeline steps
            var steps = new List<PipelineStep>
            {
                // 1. Add a document attachment (no visible annotation)
                new PipelineStep
                {
                    Action = ActionType.AddAttachment,
                    AttachmentPath = "attachment.docx",
                    AttachmentDescription = "Sample attachment"
                },

                // 2. Change a viewer preference (hide the menubar)
                new PipelineStep
                {
                    Action = ActionType.SetViewerPreference,
                    ViewerPreference = ViewerPreference.HideMenubar
                },

                // 3. Create a file‑attachment annotation on page 1
                new PipelineStep
                {
                    Action = ActionType.CreateFileAnnotation,
                    AnnotationRect = new System.Drawing.Rectangle(100, 500, 150, 150),
                    AnnotationContents = "See attached file",
                    AnnotationFilePath = "attachment.docx",
                    AnnotationPage = 1,
                    AnnotationIconName = "Paperclip"
                }
            };

            // Use PdfContentEditor facade to edit the PDF
            var editor = new PdfContentEditor();
            try
            {
                // Load the source PDF
                editor.BindPdf(inputPdf);

                // Process each configured step sequentially
                foreach (var step in steps)
                {
                    switch (step.Action)
                    {
                        case ActionType.AddAttachment:
                            // Add an attachment without a visible annotation
                            editor.AddDocumentAttachment(step.AttachmentPath, step.AttachmentDescription);
                            break;

                        case ActionType.SetViewerPreference:
                            // Change viewer preference using the integer constant from ViewerPreference
                            editor.ChangeViewerPreference(step.ViewerPreference);
                            break;

                        case ActionType.CreateFileAnnotation:
                            // Create a visible file‑attachment annotation on the specified page
                            editor.CreateFileAttachment(
                                step.AnnotationRect,
                                step.AnnotationContents,
                                step.AnnotationFilePath,
                                step.AnnotationPage,
                                step.AnnotationIconName);
                            break;
                    }
                }

                // Save the modified PDF
                editor.Save(outputPdf);
                Console.WriteLine($"Pipeline completed. Output saved to '{outputPdf}'.");
            }
            finally
            {
                // Ensure resources are released
                editor.Close();
                editor.Dispose();
            }
        }
    }
}