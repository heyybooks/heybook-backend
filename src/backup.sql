PGDMP                 
        |         	   hey-books    15.10    15.10                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398 	   hey-books    DATABASE     �   CREATE DATABASE "hey-books" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Turkish_T�rkiye.1254';
    DROP DATABASE "hey-books";
                postgres    false            �            1259    16535    Books    TABLE     �   CREATE TABLE public."Books" (
    "BookId" integer NOT NULL,
    "BookName" text,
    "Writer" text,
    "PublishingHouse" text
);
    DROP TABLE public."Books";
       public         heap    postgres    false            �            1259    16514    Cities    TABLE     U   CREATE TABLE public."Cities" (
    "CityId" integer NOT NULL,
    "CityName" text
);
    DROP TABLE public."Cities";
       public         heap    postgres    false            �            1259    16528    Listings    TABLE     �   CREATE TABLE public."Listings" (
    "ListingId" integer NOT NULL,
    "BookId" integer,
    "CityId" integer,
    "Status" boolean,
    "CreatedAt" time without time zone
);
    DROP TABLE public."Listings";
       public         heap    postgres    false            �            1259    16521    Messages    TABLE     �   CREATE TABLE public."Messages" (
    "MessageId" integer NOT NULL,
    "SenderId" integer,
    "ReceiverId" integer,
    "Content" text,
    "SentAt" time without time zone,
    "IsRead" boolean
);
    DROP TABLE public."Messages";
       public         heap    postgres    false            �            1259    16507    Users    TABLE     �   CREATE TABLE public."Users" (
    "UserId" integer NOT NULL,
    "Username" text,
    "Email" text,
    "PasswordHash" text,
    "Name" text,
    "Surname" text,
    "Role" text,
    "CreatedAt" timestamp without time zone
);
    DROP TABLE public."Users";
       public         heap    postgres    false                      0    16535    Books 
   TABLE DATA           T   COPY public."Books" ("BookId", "BookName", "Writer", "PublishingHouse") FROM stdin;
    public          postgres    false    218   -                 0    16514    Cities 
   TABLE DATA           8   COPY public."Cities" ("CityId", "CityName") FROM stdin;
    public          postgres    false    215   J                 0    16528    Listings 
   TABLE DATA           \   COPY public."Listings" ("ListingId", "BookId", "CityId", "Status", "CreatedAt") FROM stdin;
    public          postgres    false    217   g                 0    16521    Messages 
   TABLE DATA           j   COPY public."Messages" ("MessageId", "SenderId", "ReceiverId", "Content", "SentAt", "IsRead") FROM stdin;
    public          postgres    false    216   �                 0    16507    Users 
   TABLE DATA           x   COPY public."Users" ("UserId", "Username", "Email", "PasswordHash", "Name", "Surname", "Role", "CreatedAt") FROM stdin;
    public          postgres    false    214   �       }           2606    16541    Books Books_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Books"
    ADD CONSTRAINT "Books_pkey" PRIMARY KEY ("BookId");
 >   ALTER TABLE ONLY public."Books" DROP CONSTRAINT "Books_pkey";
       public            postgres    false    218            w           2606    16520    Cities Cities_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Cities"
    ADD CONSTRAINT "Cities_pkey" PRIMARY KEY ("CityId");
 @   ALTER TABLE ONLY public."Cities" DROP CONSTRAINT "Cities_pkey";
       public            postgres    false    215            {           2606    16534    Listings Listings_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public."Listings"
    ADD CONSTRAINT "Listings_pkey" PRIMARY KEY ("ListingId");
 D   ALTER TABLE ONLY public."Listings" DROP CONSTRAINT "Listings_pkey";
       public            postgres    false    217            y           2606    16527    Messages Messages_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public."Messages"
    ADD CONSTRAINT "Messages_pkey" PRIMARY KEY ("MessageId");
 D   ALTER TABLE ONLY public."Messages" DROP CONSTRAINT "Messages_pkey";
       public            postgres    false    216            u           2606    16513    Users Users_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("UserId");
 >   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "Users_pkey";
       public            postgres    false    214                  x������ � �            x������ � �            x������ � �            x������ � �            x������ � �     