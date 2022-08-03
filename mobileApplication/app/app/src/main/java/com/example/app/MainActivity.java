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
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.github.barteksc.pdfviewer.PDFView;

import java.io.File;
import java.io.FileNotFoundException;
import java.nio.file.Files;
import java.util.Scanner;

public class MainActivity extends AppCompatActivity {
    Bundle parameters;
    Button ovrf, schedule;
    String[] parametersList;
    TextView name, courseandsec, studentnum;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //parameters = getIntent().getExtras();

        parametersList = getIntent().getStringArrayExtra("param");

        name = findViewById(R.id.name);
        studentnum = findViewById(R.id.snum);
        courseandsec = findViewById(R.id.courseandsec);
        ovrf = findViewById(R.id.ovrf_btn);
        schedule = findViewById(R.id.schedule_btn);

        ovrf.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), OVRFActivity.class);
                startActivity(intent);
            }
        });

        schedule.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), ScheduleActivity.class);
                startActivity(intent);
            }
        });

        String fullname = parametersList[3] + ", " + parametersList[2] + " " + parametersList[4];
        String course = parametersList[5];
        String section = parametersList[6];
        char year = section.charAt(0);
        String courseAndSec = course + " " + section;
        String snum = parametersList[0];

        name.setText(fullname);
        studentnum.setText(snum);
        courseandsec.setText(courseAndSec);

        //Toast.makeText(getApplicationContext(), String.join(",", parametersList), Toast.LENGTH_LONG);

        DownloadPDFfromURL(year, course, snum, fullname);
    }

    private void DownloadPDFfromURL(char y, String course, String snum, String name) {
        String year = String.valueOf(y);

        new DownloadTXT()
                .execute("http://192.168.1.52/programs/semester.txt", "semester.txt");

        File sem = new File(Environment.getExternalStorageDirectory()
                + "/TXTs/" + "semester.txt");
        try {
            Scanner reader = new Scanner(sem);
            String semester = reader.nextLine();

            new DownloadPDF()
                    .execute(String.format("http://192.168.1.52/programs/Registration/Registration/Schedules/%s_year/%s/%s_sem.pdf", year, course, semester),
                            "sched.pdf");

        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

        new DownloadPDF()
                .execute(String.format("http://192.168.1.52/programs/Registration/Registration/OVRFs/%s_year/%s/%s %s.pdf", year, course, snum, name),
                        "ovrf.pdf");

    }
}