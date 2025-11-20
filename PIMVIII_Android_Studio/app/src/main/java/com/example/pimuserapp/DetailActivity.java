package com.example.pimuserapp;

import android.os.Bundle;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class DetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail);

        TextView titleView = findViewById(R.id.detailTitle);
        TextView descView = findViewById(R.id.detailDesc);

        TextView songsView = findViewById(R.id.detailSongs);

        String title = getIntent().getStringExtra("EXTRA_TITLE");
        String desc = getIntent().getStringExtra("EXTRA_DESC");

        String songs = getIntent().getStringExtra("EXTRA_SONGS");

        if (title != null) titleView.setText(title);
        if (desc != null) descView.setText(desc);

        if (songs != null) {
            songsView.setText(songs);
        } else {
            songsView.setText("Erro ao carregar lista.");
        }
    }
}