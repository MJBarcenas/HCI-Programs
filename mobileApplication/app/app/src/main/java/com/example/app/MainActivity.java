package com.example.app;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.provider.Settings;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import com.github.barteksc.pdfviewer.PDFView;

import java.io.File;
import java.nio.file.Files;

public class MainActivity extends AppCompatActivity {

    PDFView ovrf, schedule;
    Bundle parameters;
    String[] parametersList;
    TextView name, courseandsec, studentnum;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ovrf = findViewById(R.id.ovrf);
        schedule = findViewById(R.id.schedule);

        //parameters = getIntent().getExtras();

        parametersList = getIntent().getStringArrayExtra("param");

        name = findViewById(R.id.name);
        String fullname = parametersList[3] + ", " + parametersList[2] + " " + parametersList[4];
        name.setText(fullname);

        studentnum = findViewById(R.id.snum);
        studentnum.setText(parametersList[0]);

        courseandsec = findViewById(R.id.courseandsec);
        String courseAndSec = parametersList[5] + " " + parametersList[6];
        courseandsec.setText(courseAndSec);

        //Toast.makeText(getApplicationContext(), String.join(",", parametersList), Toast.LENGTH_LONG);

        DownloadPDFfromURL();

        try {
            Thread.sleep(500);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        ViewPDF();
    }

    private void DownloadPDFfromURL() {
        new DownloadPDF()
                .execute("http://192.168.1.5/programs/Registration/Registration/OVRFs/1_year/BSIT/22-00124 Barcenas, Michael Justin .pdf",
                        "ovrf.pdf");

        new DownloadPDF()
                .execute("http://192.168.1.5/programs/Registration/Registration/OVRFs/1_year/BSIT/22-00124 Barcenas, Michael Justin .pdf",
                        "sched.pdf");
    }

    public void ViewPDF() {
        File ovrfFile = new File(Environment.getExternalStorageDirectory()
                + "/PDFs/" + "ovrf.pdf");
        File schedFile = new File(Environment.getExternalStorageDirectory()
                + "/PDFs/" + "sched.pdf");

        Uri ovrfpath = Uri.fromFile(ovrfFile);

        Uri schedpath = Uri.fromFile(schedFile);

        ovrf.fromUri(ovrfpath).load();

        schedule.fromUri(schedpath).load();

        //pdfView.fromFile(pdfFile).load();
    }
}