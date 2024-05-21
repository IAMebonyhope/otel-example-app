
# .NET Example Application with OpenTelemetry

This repository contains an .NET application instrumented with OpenTelemetry to collect metrics and traces. The collected telemetry data is exported to an OpenTelemetry Collector, which forwards the data to Jaeger for tracing and Prometheus for metrics.

This example application was created with the [OpenTelemetry .Net manual instrumentation doc](https://opentelemetry.io/docs/languages/net/instrumentation/), you can follow the documentation for more understanding.

## Prerequisites

Before you begin, ensure you have the following installed:

- Docker
- Docker Compose

## Project Structure
- `no-instrumentation folder`: Contains the example application without any instrumentation or openTelemetry setup.
- `instrument-traces folder`: Builds on the no-instrumentation project and adds instrumentation for traces.
- `instrument-metrics folder`: Builds on the instrument-traces project and adds instrumentation for metrics.
- `add-collector folder`: Builds on the instrument-metrics project and uses the collector to collect the traces and metrics.    
    - `Dockerfile`: Defines how to build the ASP.NET application Docker image.
    - `docker-compose.yml`: Defines the services for the ASP.NET application, OpenTelemetry Collector, Jaeger, and Prometheus.
    - `otel-collector-config.yaml`: Configuration file for the OpenTelemetry Collector.
    - `prometheus.yml`: Configuration file for Prometheus.

## Quick Setup

### 1. Clone this repository to your local machine:

```sh
git clone https://github.com/IAMebonyhope/otel-example-app
cd otel-example-app/add-collector
```

### 2. Build and run the application using Docker Compose:

```sh
docker-compose up --build
```

### 3. Access the services:

- **ASP.NET Application**: Open your browser and navigate to `http://localhost:5000/rolldice?player=janetjane&rolls=6` to access the ASP.NET application.
- **Jaeger UI**: Open your browser and navigate to `http://localhost:16686` to access Jaeger and view traces.
- **Prometheus UI**: Open your browser and navigate to `http://localhost:9090` to access Prometheus and view metrics.

### 4. Stop the services:
press `Ctrl+C` in the terminal where `docker-compose up` is running to stop the running containers:

```sh
docker-compose down
```
