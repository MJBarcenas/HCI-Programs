package com.example.app;

import androidx.appcompat.app.AppCompatActivity;

import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;

import com.github.barteksc.pdfviewer.PDFView;

import java.io.File;

public class OVRFActivity extends AppCompatActivity {

    PDFView ovrf;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ovrfactivity);

        ovrf = findViewById(R.id.ovrf);

        ViewPDF();
    }

    public void ViewPDF() {
        File ovrfFile = new File(Environment.getExternalStorageDirectory()
                + "/PDFs/" + "ovrf.pdf");

        Uri ovrfpath = Uri.fromFile(ovrfFile);

        ovrf.fromUri(ovrfpath).load();
    }
}