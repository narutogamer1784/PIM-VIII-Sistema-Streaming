package com.example.pimuserapp;

import java.util.List; // <--- ESSE IMPORT É CRUCIAL!

public class Playlist {
    public int id;
    public String title;
    public String description;

    public List<PlaylistItem> items;
}