using System;
using System.IO;
using System.Threading;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

public class PdfSanitizer
{
    private FileSystemWatcher? _watcher;
    private readonly string _inputFolder = @"C:\PdfInput";
    private readonly string _archiveFolder = @"C:\PdfArchive";

    // Starts the watcher – equivalent to ServiceBase.OnStart
    public void Start(string[]? args = null)
    {
        // Ensure folders exist
        Directory.CreateDirectory(_inputFolder);
        Directory.CreateDirectory(_archiveFolder);

        // Set up watcher for new PDF files
        _watcher = new FileSystemWatcher(_inputFolder, "*.pdf")
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size,
            EnableRaisingEvents = true,
            IncludeSubdirectories = false
        };
        _watcher.Created += OnCreated;
    }

    // Stops the watcher – equivalent to ServiceBase.OnStop
    public void Stop()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= OnCreated;
            _watcher.Dispose();
            _watcher = null;
        }
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        // Process the file on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessPdf(e.FullPath));
    }

    private void ProcessPdf(string sourcePath)
    {
        // Wait until the file is fully written and can be opened
        const int maxAttempts = 10;
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using (FileStream fs = File.Open(sourcePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // File is ready; break out of the loop
                    break;
                }
            }
            catch (IOException)
            {
                attempt++;
                Thread.Sleep(500); // wait half a second before retry
            }
        }

        // If the file is still locked, skip processing
        if (attempt == maxAttempts)
        {
            // Log or handle as needed
            return;
        }

        string fileName = Path.GetFileName(sourcePath);
        string sanitizedPath = Path.Combine(_archiveFolder, fileName);

        try
        {
            // Load the PDF, sanitize, and save to the archive folder
            using (Document doc = new Document(sourcePath))
            {
                // Remove metadata
                doc.Info.Title = string.Empty;
                doc.Info.Author = string.Empty;
                doc.Info.Subject = string.Empty;
                doc.Info.Keywords = string.Empty;
                doc.Info.Creator = string.Empty;
                doc.Info.Producer = string.Empty;
                doc.Info.CreationDate = DateTime.MinValue;
                doc.Info.ModDate = DateTime.MinValue;

                // Remove all annotations from each page
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                {
                    Page page = doc.Pages[i];
                    page.Annotations.Clear();
                }

                // Flatten form fields (if any) to static content
                if (doc.Form != null)
                {
                    doc.Form.Flatten();
                }

                // Remove any JavaScript actions attached to the document via reflection
                RemoveDocumentJavaScriptActions(doc);

                // Save sanitized PDF to the archive folder
                doc.Save(sanitizedPath);
            }

            // Optionally delete the original file after successful sanitization
            File.Delete(sourcePath);
        }
        catch (Exception)
        {
            // Handle errors (logging omitted for brevity)
            // In case of failure, the original file remains in the input folder for later inspection
        }
    }

    /// <summary>
    /// Clears all JavaScript actions from the document's Action collection using reflection.
    /// This works across Aspose.Pdf versions where explicit properties like OnOpen, OnClose, etc., may be absent.
    /// </summary>
    private void RemoveDocumentJavaScriptActions(Document doc)
    {
        var actions = doc.Actions;
        if (actions == null) return;

        var actionType = actions.GetType();
        // Iterate over all public instance properties; if the property type is JavascriptAction and it can be written, set it to null.
        foreach (var prop in actionType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.PropertyType == typeof(JavascriptAction) && prop.CanWrite)
            {
                prop.SetValue(actions, null);
            }
        }
    }

    // Main entry point – runs as a console app for debugging or can be wrapped by a Windows Service host.
    public static void Main(string[] args)
    {
        if (Environment.UserInteractive)
        {
            // Run as console app for debugging
            var sanitizer = new PdfSanitizer();
            sanitizer.Start(args);
            Console.WriteLine("PdfSanitizer is running. Press Enter to stop...");
            Console.ReadLine();
            sanitizer.Stop();
        }
        else
        {
            // When deployed as a Windows Service, you would create a ServiceBase‑derived wrapper
            // that forwards Start/Stop to this class. The wrapper is omitted here to avoid the
            // System.ServiceProcess dependency that caused the original build error.
        }
    }
}
