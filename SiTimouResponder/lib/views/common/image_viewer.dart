import 'package:flutter/material.dart';
import 'package:photo_view/photo_view.dart';

class ImageViewer extends StatelessWidget {
  const ImageViewer({
    required this.provider,
    Key? key,
  }) : super(key: key);

  final ImageProvider provider;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.transparent,
      body: SafeArea(
        child: Container(
          constraints: BoxConstraints.expand(height: MediaQuery.of(context).size.height),
          child: PhotoView(
            imageProvider: provider,
            enableRotation: true,
            loadingBuilder: (context, event) {
              if (event == null) {
                return const Center(
                  child: Text(
                    "Loading",
                    style: TextStyle(
                      color: Colors.white,
                    ),
                  ),
                );
              }
              final value = event.cumulativeBytesLoaded / event.expectedTotalBytes!;

              debugPrint(value.toString());

              final percentage = (100 * value).floor();
              return Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    CircularProgressIndicator(
                      backgroundColor: Colors.transparent,
                      value: value,
                      valueColor: const AlwaysStoppedAnimation<Color>(Colors.white),
                    ),
                    const SizedBox(height: 12.0),
                    Text(
                      "$percentage%",
                      style: const TextStyle(color: Colors.white),
                    ),
                  ],
                ),
              );
            },
            errorBuilder: (_, __, ___) {
              return const Image(
                image: AssetImage("assets/images/img_broken.png"),
                height: 256.0,
                width: 256.0,
                alignment: Alignment.center,
                fit: BoxFit.cover,
              );
            },
          ),
        ),
      ),
    );
  }
}
