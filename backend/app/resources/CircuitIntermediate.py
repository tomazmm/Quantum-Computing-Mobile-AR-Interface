import os
from flask_restful import Resource
from flask import request
from qiskit import QuantumCircuit, transpile
from qiskit.providers.aer import Aer


class CircuitIntermediate(Resource):
    def post(self):
        try:
            # circ = QuantumCircuit(2)
            # circ.h(0)
            # circ.x(1)
            # circ.save_statevector()
            # circ.cx(0, 1)
            # circ.save_statevector(label="state1")
            # circ.h(0)
            # circ.save_statevector(label="state2")
            # # circ.measure_active()
            #
            # # circ.draw("mpl", filename="image")
            # print(*circ.data, sep="\n")
            #
            # # Transpile for simulator
            # simulator = Aer.get_backend('aer_simulator')
            # circ = transpile(circ, simulator)
            #
            # circ.draw("mpl", filename="image")
            #
            # # Run and get statevector
            # result = simulator.run(circ).result()
            # print(result)
            # # statevector = result.get_statevector()
            # # print(statevector)
            # # plot_state_city(statevector, title='Bell state')

            qasm_str = request.get_data().decode('utf-8')
            circ = QuantumCircuit.from_qasm_str(qasm_str)

            # circ = CircuitIntermediate._add_save_state_vectors(circ)

            circ.save_vectorstate()

            backend = Aer.get_backend(name="aer_simulator")

            # circ.measure_active()

            circ.draw("mpl", filename="image")

            # Transpile for simulator
            circ = transpile(circ, backend)

            # Run and get statevector
            result = backend.run(circ).result()

        except Exception as e:
            return e.args[0], 400

    @staticmethod
    def _add_save_state_vectors(circuit: QuantumCircuit) -> QuantumCircuit:
        print(*circuit.data, sep="\n")




        return circuit



