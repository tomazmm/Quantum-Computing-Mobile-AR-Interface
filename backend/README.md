# Flask backend
## Requirements
To run the backend instance you need to have preinstalled:
- python 3.7+
- pip
- docker (optional)

Run the following command inside `backend/` folder to install required libraries:
```bash
pip3 install -r requirements.txt
```

## Getting started
### Test
TBD
### Run
The easiest way to start your own instance with:
```bash
python3 app.py
```
To run an instance in a docker environment you can build the image and run the container with:
```bash
docker build -t quantum-backend .
docker run -d --name quantum-backend -p 5000:5000 quantum-backend
```

