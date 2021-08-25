import os
from flask import Flask
from flask_restful import Api

from .resources.Circuit import Circuit
from .resources.CircuitIntermediate import CircuitIntermediate



def create_app(test_config=None):
    app = Flask(__name__, instance_relative_config=True)

    # register resources
    api = Api(app)
    api.add_resource(Circuit, "/circuit")
    api.add_resource(CircuitIntermediate, "/circuit-intermediate")

    return app
