### Tubes2_Acetone

# TUGAS BESAR 2 <br>

#### IF2211 Strategi Algoritma <br>
### Maze Treasure Hunt dengan Algoritma BFS, DFS, TSP <br>

## Daftar Isi

- [Penjelasan Ringkas Program](#penjelasan-ringkas-program)
- [Penjelasan Algoritma](#penjelasan-algoritma)
- [Pre-Requisite](#pre-requisite)
- [Cara Menjalankan Program](#cara-menjalankan-program)
- [Screenshot Hasil Pengujian](#screenshot-hasil-pengujian)
- [Kontributor](#kontributor)

## Penjelasan Ringkas Program

Tuan Krabs menemukan sebuah labirin distorsi terletak tepat di bawah Krusty Krab bernama El Doremi yang Ia yakini mempunyai sejumlah harta karun di dalamnya dan tentu saja Ia ingin mengambil harta karunnya. Dikarenakan labirinnya dapat mengalami distorsi, Tuan Krabs harus terus mengukur ukuran dari labirin tersebut. Oleh karena itu, Tuan Krabs banyak menghabiskan tenaga untuk melakukan hal tersebut sehingga Ia perlu memikirkan bagaimana caranya agar Ia dapat menelusuri labirin ini lalu memperoleh seluruh harta karun dengan mudah. Program ini dibuat dengan mengimplementasikan DFS, BFS, dan TSP untuk membantu Tuan Krabs dalam menemukan seluruh harta karunnya.

## Penjelasan Algoritma

#### BFS 
Untuk menyelesaikan pencarian harta karun menggunakan dasar algoritma BFS, maka akan dibuat beberapa fungsi penunjang antara lain checkTreasure(Node, int), checkAnyChild(Node), dan BFSSearch(Node[,], int). Fungsi BFSSearch adalah fungsi utama yang akan melakukan pencarian jumlah harta karun yang telah ditemukan, node-node pada satu level yang sama dan sedang dicari, serta total semua node yang dicari. Akan dilakukan pengulangan pencarian pada level yang lebih bawah untuk setiap node apabila belum ditemukan harta karun sejumlah dengan jumlah hartakarun yang dimasukkan pada parameter.

#### DFS
Untuk menyelesaikan pencarian harta karun menggunakan dasar algoritma DFS, maka akan dibuat beberapa fungsi penunjang antara lain nowChecking(Node), DFSSearchRecursive(Node[,], int, Node, Node), dan DFSSearch(Node[,], int). Fungsi nowChecking adalah fungsi yang menerima sebuah node dari peta dan mengembalikan node yang akan dicek selanjutnya pada pencarian DFS. Fungsi ini memeriksa node anak yang belum pernah di cek mulai dari kanan, bawah, kiri, dan atas secara berurutan. Jika tidak ada anak node yang belum pernah dicek, maka fungsi ini akan mengembalikan node yang diberikan sebagai input. 

DFSSearchRecursive adalah sebuah fungsi yang melakukan pencarian DFS rekursif pada peta dan mengembalikan array dari node yang telah dilewati serta jumlah harta karun yang telah ditemukan. Fungsi ini menerima beberapa parameter yaitu peta yang akan dicari, jumlah harta karun yang ingin ditemukan, node saat ini, dan node induk. Fungsi ini melakukan pencarian pada node saat ini dengan memeriksa apakah node tersebut merupakan harta karun atau bukan. Jika iya, maka jumlah harta karun yang ditemukan akan di-increment. Jika tidak, maka fungsi nowChecking akan dipanggil untuk mendapatkan node anak yang akan dicek selanjutnya. Jika node anak yang ditemukan berbeda dengan node saat ini, maka fungsi DFSSearchRecursive akan dipanggil kembali pada node anak tersebut. Fungsi ini akan diulang terus menerus sampai semua harta karun yang diinginkan telah ditemukan atau tidak ada node lagi yang dapat dikunjungi. Fungsi ini mengembalikan array dari node yang telah dilewati dan jumlah harta karun yang telah ditemukan. 

DFSSearch adalah sebuah fungsi yang melakukan pencarian DFS pada peta dengan memanggil fungsi DFSSearchRecursive pada node awal (biasanya node pertama pada peta). Fungsi ini mengembalikan array dari node yang telah dilewati. Fungsi ini digunakan untuk memulai pencarian DFS pada peta.

#### TSP
Untuk menyelesaikan pencarian harta karun menggunakan dasar algoritma TSP, maka akan dibuat beberapa fungsi penunjang antara lain restartCheck(Node[,]), TSPSteps(Node, Node), dan TSPSearch(Node[,], int). Fungsi restartCheck akan mengulang status cek pada semua node dalam map. Kegunaannya adalah untuk mengembalikan node pada kondisi semula sebelum dilakukan pencarian jalur terpendek. Fungsi TSPSteps akan mencari jalur terpendek dari suatu node start ke node finish pada peta. Kegunaannya adalah untuk mencari jalur terpendek dari satu titik ke titik lainnya. Jika node yang dicek sama dengan finish, fungsi akan menghentikan pencarian. 

TSPSearch adalah fungsi utama yang akan melakukan pencarian jalur terpendek untuk mengunjungi beberapa titik pada peta. TSPSearch akan mencari jalur terpendek dengan cara mengecek setiap node yang merupakan titik-titik harta karun (yang ingin dikunjungi). Kemudian node-node tersebut diurutkan berdasarkan jarak terdekat dan diproses satu per satu untuk mencari jalur terpendek yang melalui semua titik hata karun tersebut. Setelah itu, akan dilakukan kembali langkah yang sama untuk kembali ke titik awal pada map. Dalam program ini, terdapat beberapa fungsi lain seperti checkNode() untuk mengecek node, concatNode() untuk  menggabungkan node, dan relativeDistance() untuk menghitung jarak relatif antara suatu node dengan node lainnya.

## Pre-Requisite
* .NET MAUI dapat diunduh melalui `https://dotnet.microsoft.com/en-us/apps/maui`
* Visual Studio versi terbaru yang dapat diunduh melalui `https://visualstudio.microsoft.com/downloads/`

## Cara Menjalankan Program
1. Pastikan device Anda telah memenuhi pre-requisite untuk menjalankan program kemudian clone repository pada local folder Anda.
2. Buka repository yang telah di clone dengan Visual Studio
3. Masuk pada bagian folder Program dan pilih child folder yang sesuai dengan device yang Anda gunakan.
4. Lakukan debuggin dan running program dengan menekan tombol start di bagian atas.
5. Program akan berjalan dan Anda dapat menjalankan program dengan memilih button jenis penelusuran dan menekan button run dan visualize.

## Screenshot Hasil Pengujian

<img src="./program.png" width="450">

## Kontributor

13521009 Christophorus Dharma Winata <br>
13521125 Asyifa Nurul Shafira <br>
13521133 Cetta Reswara Parahita
